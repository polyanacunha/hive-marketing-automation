
namespace Hive.Domain.Entities
{
    public class ClientProfile 
    {
        public string Id { get; private set; }
        public MarketSegment MarketSegment { get; private set; }
        public int MarketSegmentId { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public int TargetAudienceId { get; private set; }
        public string CompanyName { get; private set; }
        public string TaxId { get; private set; }
        public string? WebSiteUrl { get; private set; }
        public virtual ICollection<MidiaProduction> Midias { get; private set; } = new List<MidiaProduction>();
        public virtual ICollection<Campaign> Campaigns { get; private set; } = new List<Campaign>();

        private ClientProfile() { } 

        public ClientProfile(string id, MarketSegment marketSegment, int marketSegmentId, TargetAudience targetAudience, int targetAudienceId, string companyName, string taxId, string? webSiteUrl)
        {
            Id = id;
            MarketSegment = marketSegment;
            MarketSegmentId = marketSegmentId;
            TargetAudience = targetAudience;
            TargetAudienceId = targetAudienceId;
            CompanyName = companyName;
            TaxId = taxId;
            WebSiteUrl = webSiteUrl;
        }
    }
}   
