using Hive.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Services
{
    public class SceneProcessingJob : IJob
    {
        private const int MAX_RETRIES = 3;
        private readonly IGenerateVideosByScenes _processVideo;
        private readonly ILogger<SceneProcessingJob> _logger;

        public SceneProcessingJob(IGenerateVideosByScenes videoCreationProcessService, ILogger<SceneProcessingJob> logger)
        {
            _processVideo = videoCreationProcessService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                if (int.TryParse(context.JobDetail.JobDataMap.GetString("SceneJobId"), out var sceneJobId))
                {
                    int.TryParse(context.JobDetail.JobDataMap.GetString("mediaId"), out var mediaId);
                    // Chama o método específico no orquestrador para processar esta cena.
                    await _processVideo.ProcessSingleScene(sceneJobId, mediaId, context.CancellationToken);
                }
            }
            catch (Exception ex) 
            {
                // Usa o contador de retentativas NATIVO do Quartz.
                if (context.RefireCount < MAX_RETRIES)
                {
                    _logger.LogWarning(ex, "Falha na execução do Job {JobKey}. Tentativa {RefireCount}.", context.JobDetail.JobDataMap.GetString("SceneJobId"),
                    context.RefireCount + 1);

                    // Se ainda podemos tentar de novo, lançamos esta exceção especial.
                    // Ela diz ao Quartz: "A execução falhou, mas eu quero que você me execute de novo imediatamente."
                    throw new JobExecutionException(ex, refireImmediately: true);
                }
                
                _logger.LogError("Job {JobKey} falhou permanentemente após {MaxRetries} tentativas.", context.JobDetail.Key, MAX_RETRIES);
                var productionId = int.Parse(context.JobDetail.JobDataMap.GetString("SceneJobId")!);
                await _processVideo.MarkSceneAsFailedPermanently(productionId, context.CancellationToken);
                
            }
        }
    }
}
