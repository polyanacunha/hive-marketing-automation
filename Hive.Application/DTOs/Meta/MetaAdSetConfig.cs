using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Application.DTOs.Meta
{
    public class MetaAdSetConfig
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("campaign_id")]
        public string CampaignId { get; set; } // Será preenchido após a criação da campanha

        [JsonPropertyName("daily_budget")]
        public long DailyBudgetInCents { get; set; } // A API espera o orçamento em centavos

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; } // Formato: "yyyy-MM-ddTHH:mm:ss-0300"

        [JsonPropertyName("end_time")]
        public string? EndTime { get; set; }

        [JsonPropertyName("billing_event")]
        public string BillingEvent { get; set; }

        [JsonPropertyName("optimization_goal")]
        public string OptimizationGoal { get; set; }

        [JsonPropertyName("targeting")]
        public MetaTargetingConfig Targeting { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "PAUSED";
    }

    public class MetaTargetingConfig
    {
        [JsonPropertyName("geo_locations")]
        public MetaGeoLocationsConfig GeoLocations { get; set; }

        [JsonPropertyName("age_min")]
        public int AgeMin { get; set; } = 18;

        [JsonPropertyName("age_max")]
        public int AgeMax { get; set; } = 65;

        [JsonPropertyName("genders")]
        public List<int>? Genders { get; set; } // 1 = Homem, 2 = Mulher

        [JsonPropertyName("publisher_platforms")]
        public List<string> PublisherPlatforms { get; set; } = new List<string> { "facebook", "instagram" };

        // Lista de interesses, com IDs já resolvidos
        [JsonPropertyName("flexible_spec")]
        public List<MetaFlexibleSpec> FlexibleSpec { get; set; }
    }

    public class MetaFlexibleSpec
    {
        [JsonPropertyName("interests")]
        public List<MetaInterestConfig> Interests { get; set; }
    }

    public class MetaInterestConfig
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class MetaGeoLocationsConfig
    {
        // Para segmentação por nome (país, região, cidade)
        [JsonPropertyName("countries")]
        public List<string>? Countries { get; set; }

        [JsonPropertyName("regions")]
        public List<MetaLocationKey>? Regions { get; set; }

        [JsonPropertyName("cities")]
        public List<MetaLocationKey>? Cities { get; set; }

        // Para segmentação por coordenadas (pino no mapa)
        [JsonPropertyName("custom_locations")]
        public List<MetaCustomLocationConfig>? CustomLocations { get; set; }
    }

    public class MetaLocationKey
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class MetaCustomLocationConfig
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("radius")]
        public int Radius { get; set; }

        [JsonPropertyName("distance_unit")]
        public string DistanceUnit { get; set; } = "kilometer";
    }


    public class PromotedObject
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string PageId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ApplicationId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ObjectStoreUrl { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ProductCatalogId { get; set; }

    }
}
