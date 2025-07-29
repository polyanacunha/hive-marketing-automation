using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Enum;

namespace Hive.Application.Services.Meta.AdSetMapper
{
    public class AwaranessAdSetStrategy : IAdSetConfigStrategy
    {
        public ObjectiveCampaignEnum Objective => ObjectiveCampaignEnum.AWARENESS;
        private readonly IMetaUtilsService _metaUtilsService;

        public AwaranessAdSetStrategy(IMetaUtilsService metaUtilsService)
        {
            _metaUtilsService = metaUtilsService;
        }
        public async Task<AdSetConfig> BuildAdSetAsync(TargetingStrategy strategy, Campaign campaign, string campaignId, string accessToken)
        {

            var metaLocations = await _metaUtilsService.MapLocationsMeta(strategy.Audience.Locations, accessToken);

            return new AdSetConfig
            {
                CampaignId = campaignId,
                Name = "HIVE/AWARANESS",
                Status = "PAUSED",
                BillingEvent = "IMPRESSIONS",
                OptimizationGoal = "REACH",
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
            };
        }
    }
}
