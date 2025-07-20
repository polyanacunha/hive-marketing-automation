using Hive.Application.Interfaces;
using Quartz;

namespace Hive.Infra.Data.Services
{
    [DisallowConcurrentExecution]
    public class VideoStitchingJob : IJob
    {
        private readonly IGenerateVideosByScenes _processVideo;

        public VideoStitchingJob(IGenerateVideosByScenes processService)
        {
            _processVideo = processService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (int.TryParse(context.MergedJobDataMap.GetString("ProductionId"), out var productionId))
            {
                // Chama o método final do servico para fazer a montagem.
                await _processVideo.StitchFinalVideo(productionId, context.CancellationToken);
            }
        }
    }
}
