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
        public Guid ClientProfileId { get; private set; }
        public ClientProfile ClientProfile { get; private set; }
        public string Prompt { get; private set; }

        public List<string>? InputImageUrl = new();
        public JobStatus Status { get; private set; } 
        public string? ExternalPlatformfJobId { get; private set; }
        public string? FinalVideoUrl { get; private set; }
        public AssetType AssetType { get; private set; }

        private JobGeneration()
        {
        }

        public JobGeneration(
            Guid clientProfileId, 
            ClientProfile clientProfile, 
            string prompt, 
            List<string>? inputImageUrl, 
            string? externalPlatformfJobId, 
            string? finalVideoUrl,
            AssetType assetType)
        {
            ClientProfileId = clientProfileId;
            ClientProfile = clientProfile;
            Prompt = prompt;
            InputImageUrl = inputImageUrl;
            Status = JobStatus.PENDING;
            ExternalPlatformfJobId = externalPlatformfJobId;
            FinalVideoUrl = finalVideoUrl;
            AssetType = assetType;
        }


    }
}
