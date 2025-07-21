using Hive.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class MidiaProduction : Entity
    {
        public string ClientProfileId { get; private set; }
        public ClientProfile ClientProfile { get; private set; }
        public string SystemPrompt { get; private set; }
        public string UserPrompt { get; private set; } 
        public string? GeneratedScriptJson { get; private set; }
        public ProductionStatus Status { get; private set; }
        public string? FinalVideoUrl { get; private set; }
        public virtual ICollection<ImageUrl> InputImageUrl { get; private set; } = new List<ImageUrl>();
        public virtual ICollection<JobGeneration> JobsGenerations { get; private set; } = new List<JobGeneration>();

        private MidiaProduction() {}

        public MidiaProduction(string clientProfileId, string systemPrompt, string userPrompt, ICollection<ImageUrl> inputImages)
        {
            ClientProfileId = clientProfileId;
            SystemPrompt = systemPrompt;
            UserPrompt = userPrompt;
            Status = ProductionStatus.Pending;
            InputImageUrl = inputImages;
        }

        public void MarkToScriptGenerated()
        {
            Status = ProductionStatus.ScriptGenerated;
        }
        public void MarkToCompleted()
        {
            Status = ProductionStatus.Completed;
        }
        public void MarkToFailed()
        {
            Status = ProductionStatus.Failed;
        }
        public void MarkToClipsGenerating()
        {
            Status = ProductionStatus.ClipsGenerating;
        }
        public void AddGeneratedScriptJson(string script)
        {
            GeneratedScriptJson = script;
        }
        public void SetFinalVideourl(string url)
        {
            FinalVideoUrl = url;
        }
    }
}
