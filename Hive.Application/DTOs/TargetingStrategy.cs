using Hive.Application.UseCases.Campaigns.Meta.CreateCampaign;
using Hive.Domain.Entities;
using System.Text.Json.Serialization;

namespace Hive.Application.DTOs
{
    public record CampaignStrategy
    {

        public Campaign? Campaign { get; set; }

        [JsonPropertyName("targeting_strategy")]
        public TargetingStrategy TargetingStrategy { get; set; }

        [JsonPropertyName("creative_suggestions")]
        public CreativeSuggestions CreativeSuggestions { get; set; }

        [JsonPropertyName("strategy_justification")]
        public string StrategyJustification { get; set; }

        public MetaParamerters? MetaParamerters { get; set; }
    }

    public record MetaParamerters
    {
        public string AccountId { get; set; }
        public string? PageId { get; set; }
        public string? PixelId { get; set; }
        public string? InstagramActorId { get; set; }
        public string? ApplicationId { get; set; }
        public string? ObjectStoreUrl { get; set; }
        public string? FormId { get; set; }
        public string? PrivacyPolicyUrl { get; set; }
        public string? ProductCatalogId { get; set; }
        public string? ProductSetId { get; set; }
        public List<string>? UserOs { get; set; }
    }

    public record TargetingStrategy
    {
        [JsonPropertyName("audience")]
        public Audience Audience { get; set; }

        [JsonPropertyName("placements_suggestion")]
        public List<string> PlacementsSuggestion { get; set; }
    }

    public record Audience
    {
        [JsonPropertyName("min_age")]
        public int MinAge { get; set; }

        [JsonPropertyName("max_age")]
        public int MaxAge { get; set; }

        [JsonPropertyName("genders")]
        public int Genders { get; set; }

        [JsonPropertyName("interests")]
        public List<string> Interests { get; set; }

        [JsonPropertyName("locations")]
        public List<Location> Locations { get; set; }

        public List<string> UserOs { get; set; }
    }

    public record Location
    {
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("regions")]
        public List<string> Region { get; set; }

        [JsonPropertyName("city")]
        public List<City> Cities { get; set; }
    }

    public record City 
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("radius")]
        public int Radius { get; set; }
    }

    public record CreativeSuggestions
    {
        [JsonPropertyName("suggested_format")]
        public string SuggestedFormat { get; set; }

        [JsonPropertyName("central_theme")]
        public string CentralTheme { get; set; }

        [JsonPropertyName("headlines")]
        public List<string> Headlines { get; set; }

        [JsonPropertyName("descriptions")]
        public List<string> Descriptions { get; set; }

        [JsonPropertyName("calls_to_action")]
        public List<string> CallsToAction { get; set; }

        [JsonPropertyName("hashtags")]
        public List<string> Hashtags { get; set; }
    }
}

