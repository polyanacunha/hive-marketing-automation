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

        public MetaAdSetConfig BuildAdSetAsync(CampaignStrategy campaignStrategy, string campaignId, MetaGeoLocationsConfig locations, List<MetaInterestConfig> interests)
        {
            return new MetaAdSetConfig
            {
                CampaignId = campaignId,
                Name = "HIVE/APP_PROMOTION",
                Status = "PAUSED",
                BillingEvent = "IMPRESSIONS",
                OptimizationGoal = "APP_INSTALLS",
                StartTime = campaignStrategy.Campaign!.PeriodRange.Initial.ToString("yyyy-MM-ddTHH:mm:sszzz").Replace(":", ""),
                EndTime = campaignStrategy.Campaign!.PeriodRange.Initial.ToString("yyyy-MM-ddTHH:mm:sszzz").Replace(":", ""),

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

                    UserOs = campaignStrategy.CustomParamerters!.UserOs!
                },

                PromotedObject = new()
                {
                    ApplicationId = campaignStrategy.CustomParamerters!.ApplicationId!,
                    ObjectStoreUrl = campaignStrategy.CustomParamerters!.ObjectStoreUrl!
                }
            };
        }
    }
}
