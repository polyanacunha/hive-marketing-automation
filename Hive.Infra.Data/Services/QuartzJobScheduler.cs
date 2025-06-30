using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Services
{
    public class QuartzJobScheduler : IBackgroundScheduler
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<QuartzJobScheduler> _logger;

        public QuartzJobScheduler(ISchedulerFactory schedulerFactory, ILogger<QuartzJobScheduler> logger)
        {
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task ScheduleScriptGeneration(int midiaProductionId)
        {
            _logger.LogInformation("Processando Job de geracao de roteiro referente a midia {Id}", midiaProductionId);

            IScheduler scheduler = await _schedulerFactory.GetScheduler();

            IJobDetail job = JobBuilder.Create<ScriptGenerationJob>()
                .WithIdentity($"script-gen-{midiaProductionId}")
                .UsingJobData("ProductionId", midiaProductionId.ToString())
                .Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task ScheduleSceneProcessingJob(int sceneJobId, int mediaId, string groupName)
        {
            _logger.LogInformation("Processando Job-{sceneJobId} de geracao de cena referente a midia-{midiaId}", sceneJobId, mediaId);

            var scheduler = await _schedulerFactory.GetScheduler();

            IJobDetail sceneJob = JobBuilder.Create<SceneProcessingJob>()
                .WithIdentity($"scene-{sceneJobId}", $"production-{groupName}") 
                .UsingJobData("SceneJobId", sceneJobId.ToString())
                .UsingJobData("mediaId", mediaId.ToString())
                .UsingJobData("RetryCount", 0)
                .Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().Build();

            await scheduler.ScheduleJob(sceneJob, trigger);
        }

        public async Task ScheduleStitchingJob(int mediaProductionId)
        {
            _logger.LogInformation("Processando Job de agendamento de juncao de videos referente a midia-{videoProductionId}", mediaProductionId);
            var scheduler = await _schedulerFactory.GetScheduler();

            IJobDetail stitchJob = JobBuilder.Create<VideoStitchingJob>()
                .WithIdentity($"stitch-{mediaProductionId}")
                .UsingJobData("ProductionId", mediaProductionId.ToString())
                .StoreDurably()
                .Build();

            await scheduler.AddJob(stitchJob, replace: true);
        }
    }
}
