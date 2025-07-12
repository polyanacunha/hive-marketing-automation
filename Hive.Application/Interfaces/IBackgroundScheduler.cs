using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IBackgroundScheduler
    {
        Task ScheduleScriptGeneration(int mediaProductionId);
        Task ScheduleSceneProcessingJob(Guid sceneJobId, int mediaId, string groupName);
        Task ScheduleStitchingJob(int mediaProductionId);
    }
}
