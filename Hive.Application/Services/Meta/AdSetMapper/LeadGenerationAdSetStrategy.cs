using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Enum;

namespace Hive.Application.Services.Meta.AdSetMapper
{
    public class LeadGenerationAdSetStrategy : IAdSetConfigStrategy
    {
        public ObjectiveCampaignEnum Objective => ObjectiveCampaignEnum.LEADS;
        private readonly IMetaUtilsService _metaUtilsService;

        public LeadGenerationAdSetStrategy(IMetaUtilsService metaUtilsService)
        {
            _metaUtilsService = metaUtilsService;
        }

        public async Task<AdSetConfig> BuildAdSetAsync(TargetingStrategy strategy, Campaign campaign, string campaignId, string accessToken)
        {

            var interests = await _metaApiService.SearchInterests(strategy.Audience.Interests, accessToken);

            var metaLocations = await _metaUtilsService.MapLocationsMeta(strategy.Audience.Locations, accessToken);

            return new AdSetConfig
            {
                CampaignId = campaignId,
                Name = "HIVE/LEADS",
                Status = "PAUSED",
                BillingEvent = "IMPRESSIONS",
                OptimizationGoal = "LEAD_GENERATION",
                StartTime = _metaUtilsService.FormatForMetaApi(campaign.PeriodRange.Initial),
                EndTime = _metaUtilsService.FormatForMetaApi(campaign.PeriodRange.End),

                Targeting = new()
                {
                    MinAge = strategy.Audience.MinAge > 13 ? strategy.Audience.MinAge : 13,
                    MaxAge = strategy.Audience.MaxAge < 65 ? strategy.Audience.MaxAge : 65,
                    Genders = strategy.Audience.Genders == 0 ? [1, 2] : [strategy.Audience.Genders],
                    Interests = interests.Value,
                    GeoLocations = metaLocations

                },

                PromotedObject = new()
                {
                    formid
                }

            };
        }
    }
}
