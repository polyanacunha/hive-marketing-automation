using Hive.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class JobGeneration : Entity
    {
        public int MidiaProductionId { get; private set; }
        public MidiaProduction MidiaProduction { get; private set; }
        public string Prompt { get; private set; }
        public JobStatus Status { get; private set; } 
        public string? ExternalJobId { get; private set; }
        public string? ExternalMediaUrl { get; private set; }
        public AssetType AssetType { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        private JobGeneration()
        {
        }

        public JobGeneration(
            int midiaProductionId,
            MidiaProduction midiaProduction, 
            string prompt, 
            AssetType assetType)
        {
            MidiaProductionId = midiaProductionId;
            MidiaProduction = midiaProduction;
            Prompt = prompt;
            Status = JobStatus.PENDING;
            AssetType = assetType;


            CreatedAt = DateTime.UtcNow;   
        }

        public void MarkToProcessing()
        {
            Status = JobStatus.PROCESSING;
        }
        public void MarkToCompleted()
        {
            CompletedAt = DateTime.UtcNow;
            Status = JobStatus.COMPLETED;
        }
        public void MarkToFailed()
        {
            Status = JobStatus.FAILED;
        }
        public void SetExternalJobId(string id)
        {
            ExternalJobId = id;
        }
        public void SetExternalMediaUrl(string url)
        {
            ExternalMediaUrl = url;
        }
    }
}
