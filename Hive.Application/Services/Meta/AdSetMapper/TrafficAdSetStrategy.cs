using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Domain.Enum;

namespace Hive.Application.Services.Meta.AdSetMapper
{
    public class TrafficAdSetStrategy : IAdSetConfigStrategy
    {
        public ObjectiveCampaignEnum Objective => ObjectiveCampaignEnum.TRAFFIC;

        public MetaAdSetConfig BuildAdSetAsync(CampaignStrategy campaignStrategy, string campaignId, MetaGeoLocationsConfig location, List<MetaInterestConfig> interests)
        {
            return new MetaAdSetConfig
            {
                CampaignId = campaignId,
                Name = "HIVE/LEADS",
                Status = "PAUSED",
                BillingEvent = "IMPRESSIONS",
                OptimizationGoal = "LEAD_GENERATION",
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

                    GeoLocations = location
                },
            };
        }
    }
}
