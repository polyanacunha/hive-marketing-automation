using Hive.Domain.Enum;
using Hive.Domain.Validation;
using Hive.Domain.ValueObjects;


namespace Hive.Domain.Entities
{
    public class Campaign : Entity
    {
        public string ClientProfileId { get; private set; }
        public string CampaignName { get; private set; }
        public string ExternalCampaignName { get; private set; }
        public ObjectiveCampaign ObjectiveCampaign { get; private set; }
        public int ObjectiveCampaignId { get; private set; }
        public Budget Budget { get; private set; }
        public PeriodRange PeriodRange { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public StatusCampaign Status { get; private set; }

        private Campaign()
        {
        }

        public Campaign(
            string clientProfileId,
            string campaignName, 
            string externalCampaignName, 
            ObjectiveCampaign objectiveCampaign, 
            int objectiveCampaignId, 
            int budget, 
            DateTime initial, 
            DateTime end)
        {
            ClientProfileId = clientProfileId;
            CampaignName = campaignName;
            ExternalCampaignName = externalCampaignName;
            ObjectiveCampaign = objectiveCampaign;
            ObjectiveCampaignId = objectiveCampaignId;
            Budget = Budget.Create(budget);
            PeriodRange = new PeriodRange(initial, end);
            CreatedAt = DateTime.UtcNow;
            Status = StatusCampaign.DRAFT;
        }

        public DateTime GetInitialPeriod() 
        {
            return PeriodRange.Initial;
        }

        public DateTime GetEndPeriod()
        {
            return PeriodRange.End;
        }

        public void SetPeriodRange(DateTime initial, DateTime end)
        {
            PeriodRange = new PeriodRange(initial, end);
        }

        public void SetBudget(int budget)
        {
            Budget = Budget.Create(budget);
        }
    }
}
