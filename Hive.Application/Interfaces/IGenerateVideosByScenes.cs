using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IGenerateVideosByScenes
    {
        Task GenerateScript(int videoProductionId, CancellationToken cancellationToken);
        Task ProcessSingleScene(Guid sceneJobId, int mediaId, CancellationToken cancellationToken);
        Task StitchFinalVideo(int videoProductionId, CancellationToken cancellationToken);
        Task MarkSceneAsFailedPermanently(Guid videoProductionId, CancellationToken cancellationToken);
    }
}
