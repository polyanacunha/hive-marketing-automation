using Hive.Domain.Enum;
using Hive.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Hive.Infra.Data.Services
{
    public class SceneCompletionListener : IJobListener
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SceneCompletionListener> _logger;


        public SceneCompletionListener(IServiceProvider serviceProvider, ILogger<SceneCompletionListener> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public string Name => "SceneCompletionListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            if (context.JobDetail.JobType != typeof(SceneProcessingJob))
            {
                return;
            }

            await using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var midiaProductionRepository = scope.ServiceProvider.GetRequiredService<IMidiaProductionRepository>();

                var productionId = int.Parse(context.JobDetail.Key.Group.Split('-').Last());

                var production = await midiaProductionRepository.GetByIdWithJobsAsync(productionId)
                    ?? throw new Exception("Midia not found");

                if (production.JobsGenerations.All(j => j.Status == JobStatus.COMPLETED))
                {
                    // ...dispara o job de montagem que estava esperando!
                    _logger.LogInformation("Processando Job juncao de videos referente a midia-{productionId}", productionId);
                    var stitchJobKey = new JobKey($"stitch-{productionId}");
                    await context.Scheduler.TriggerJob(stitchJobKey, cancellationToken);
                }
            }
        }
    }
}