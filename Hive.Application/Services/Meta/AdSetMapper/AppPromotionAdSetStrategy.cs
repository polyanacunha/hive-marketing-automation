using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Enum;
using Hive.Domain.Validation;


namespace Hive.Application.Services.Meta.AdSetMapper
{
    public class AppPromotionAdSetStrategy : IAdSetConfigStrategy
    {
        public ObjectiveCampaignEnum Objective => ObjectiveCampaignEnum.APP_PROMOTION;
        private readonly IMetaUtilsService _metaUtilsService;

        public AppPromotionAdSetStrategy(IMetaUtilsService metaUtilsService)
        {
            _metaUtilsService = metaUtilsService;
        }

        public MetaAdSetConfig BuildAdSetAsync(CampaignStrategy campaignStrategy, string campaignId, MetaGeoLocationsConfig locations, List<MetaInterestConfig> interests)
        {
            return new MetaAdSetConfig
            {
                CampaignId = campaignId,
                Name = "HIVE/APP_PROMOTION",
                Status = "PAUSED",
                BillingEvent = "IMPRESSIONS",
                OptimizationGoal = "APP_INSTALLS",
                StartTime = _metaUtilsService.FormatForMetaApi(campaignStrategy.Campaign!.PeriodRange.Initial),
                EndTime = _metaUtilsService.FormatForMetaApi(campaignStrategy.Campaign!.PeriodRange.Initial),

                Targeting = new()
                {

                    AgeMin = campaignStrategy.TargetingStrategy.Audience.MinAge > 13 ? campaignStrategy.TargetingStrategy.Audience.MinAge : 13,

                    AgeMax = campaignStrategy.TargetingStrategy.Audience.MaxAge < 65 ? campaignStrategy.TargetingStrategy.Audience.MaxAge : 65,

                    Genders = campaignStrategy.TargetingStrategy.Audience.Genders == 0 ? [1, 2] : [campaignStrategy.TargetingStrategy.Audience.Genders],

                    FlexibleSpec = new List<MetaFlexibleSpec>
                        {
                            new MetaFlexibleSpec
                            {
                                Interests = interests
                            }
                        },

                    GeoLocations = locations,

                    UserOs = campaignStrategy.CustomParamerters.UserOs
                },

                PromotedObject = new()
                {
                    ApplicationId = campaign.ApplicationId,
                    ObjectStoreUrl = campaign.ObjectStoreUrl
                }

            }
        }
    }
}
