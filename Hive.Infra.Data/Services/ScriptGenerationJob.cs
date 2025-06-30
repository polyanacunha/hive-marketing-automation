using Hive.Application.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Services
{
    [DisallowConcurrentExecution]
    public class ScriptGenerationJob : IJob
    {
        private readonly IGenerateVideosByScenes _processVideo;

        public ScriptGenerationJob(IGenerateVideosByScenes videoCreationService)
        {
            _processVideo = videoCreationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var productionId = int.Parse(context.MergedJobDataMap.GetString("ProductionId")!);
            // Chama o "Chefe de Produção" para fazer o trabalho real
            await _processVideo.GenerateScript(productionId, context.CancellationToken);
        }
    }
}
 