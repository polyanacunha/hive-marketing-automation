using Hive.Domain.Enum;
using Hive.Domain.Validation;
using Hive.Domain.ValueObjects;


namespace Hive.Domain.Entities
{
    public class Campaign : Entity
    {
        public string ClientProfileId { get; private set; }
        public string CampaignName { get; private set; }
        public ObjectiveCampaignEnum ObjectiveCampaign { get; private set; }
        public Budget Budget { get; private set; }
        public string ProdutoDescription { get; private set; }
        public PeriodRange PeriodRange { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public StatusCampaign Status { get; private set; }

        
        private Campaign()
        {
        }

        public Campaign(
            string clientProfileId,
            string campaignName, 
            ObjectiveCampaignEnum objectiveCampaign, 
            string produtoDescription,
            int budget, 
            DateTime initial, 
            DateTime end)
        {
            ClientProfileId = clientProfileId;
            CampaignName = campaignName;
            ObjectiveCampaign = objectiveCampaign;
            Budget = Budget.Create(budget);
            ProdutoDescription = produtoDescription;
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
