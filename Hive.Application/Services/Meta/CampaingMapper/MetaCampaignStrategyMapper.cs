using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Enum;
using Hive.Domain.Interfaces;


namespace Hive.Application.Services.Meta.CampaingMapper
{
    public class MetaCampaignStrategyMapper : IMetaCampaignStrategyMapper
    {
        private readonly IDictionary<ObjectiveCampaignEnum, IAdSetConfigStrategy> _adSetStrategies;
        private readonly IMetaApiService _metaApiService;

        public MetaCampaignStrategyMapper(IEnumerable<IAdSetConfigStrategy> adSetStrategies, IMetaApiService metaApiService)
        {
            _adSetStrategies = adSetStrategies switch
            {
                var list when list != null => list.ToDictionary(
                    strategy => strategy.Objective,
                    strategy => strategy),
                _ => throw new ArgumentNullException(nameof(adSetStrategies))
            };
            _metaApiService = metaApiService;
        }

        public async Task<string> CreateMetaCampaign(CampaignStrategy campaingStrategy, string accessToken, string AccountId)
        {
            var campaignMeta = MapCampaignMeta(campaingStrategy.Campaign!);
;
            var campaignId = await _metaApiService.CreateCampaign(campaignMeta, accessToken, AccountId);

            var adSetMeta = await MapToAdset(campaingStrategy, campaignId.Value!, accessToken);

            var adSetId = await _metaApiService.CreateAdSet(adSetMeta, accessToken);

            return campaignId.Value!;

        }

        private MetaCampaignConfig MapCampaignMeta(Campaign campaign)
        {
            return new MetaCampaignConfig
            {
                Objective = DetermineObjective(campaign.ObjectiveCampaign),
                Name = $"",
                Status = "PAUSED",
                SpecialAdCategories = new() { "NONE" }
            };
        }

        public async Task<MetaAdSetConfig> MapToAdset(CampaignStrategy campaingStrategy, string campaignId, string accessToken)
        {
            if (_adSetStrategies.TryGetValue(campaingStrategy.Campaign!.ObjectiveCampaign, out var strategyImpl))
            {
                var interestsTask = _metaApiService.SearchInterestsBatch(
                    campaingStrategy.TargetingStrategy.Audience.Interests,
                    accessToken
                );

                var locationsTask = MapLocationsMeta(
                    campaingStrategy.TargetingStrategy.Audience.Locations,
                    accessToken
                );

                await Task.WhenAll(interestsTask, locationsTask);

                var adSetMeta = strategyImpl.BuildAdSetAsync(campaingStrategy, campaignId, locationsTask.Result, interestsTask.Result.Value!);

                return adSetMeta;
            }

            throw new Exception($"Nenhuma estratégia de AdSet configurada para o objetivo: {campaingStrategy.Campaign!.ObjectiveCampaign}");
        }

        private string DetermineObjective(ObjectiveCampaignEnum objective) 
        {
            return objective switch
            {
                ObjectiveCampaignEnum.AWARENESS => "OUTCOME_AWARENESS",
                ObjectiveCampaignEnum.TRAFFIC => "OUTCOME_TRAFFIC",
                ObjectiveCampaignEnum.ENGAGEMENT => "OUTCOME_ENGAGEMENT",
                ObjectiveCampaignEnum.LEADS => "OUTCOME_LEADS",
                ObjectiveCampaignEnum.SALES => "OUTCOME_SALES",
                ObjectiveCampaignEnum.APP_PROMOTION => "OUTCOME_APP_PROMOTION",
                _ => throw new Exception("Error ao mapear objetivo de campanha para api da Meta.")
            };
        }

        public async Task<MetaGeoLocationsConfig> MapLocationsMeta(List<Location> locations, string accessToken)
        {
            var metaLocation = new MetaGeoLocationsConfig
            {
                Countries = new List<string>(),
                Regions = new List<MetaLocationKey>(),
                CustomLocations = new List<MetaCustomLocationConfig>()
            };

            if (locations == null || !locations.Any())
            {
                return metaLocation;
            }

            // 2. Processe os dados que NÃO precisam de chamada de API primeiro.
            // Use LINQ para fazer isso de forma mais concisa.
            metaLocation.Countries.AddRange(locations.Select(loc => loc.CountryCode));

            var customLocations = locations.SelectMany(loc => loc.Cities.Select(c => new MetaCustomLocationConfig
            {
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                Radius = c.Radius,
                DistanceUnit = "kilometer"
            }));
            metaLocation.CustomLocations.AddRange(customLocations);

            // 3. Crie uma lista de TAREFAS para todas as buscas de região, sem executá-las ainda.
            var regionSearchTasks = locations
                .SelectMany(loc => loc.Region.Select(regionName =>
                    _metaApiService.SearchRegion(regionName, loc.CountryCode, accessToken)))
                .ToList();

            // 4. Execute TODAS as tarefas em paralelo e aguarde a conclusão de todas.
            var regionResults = await Task.WhenAll(regionSearchTasks);

            // 5. Processe os resultados, adicionando apenas os que foram bem-sucedidos.
            var successfulRegionKeys = regionResults
                .Where(result => result.IsSuccess)
                .Select(result => new MetaLocationKey { Key = result.Value });

            metaLocation.Regions.AddRange(successfulRegionKeys);
            return metaLocation;
        }

    }
}
