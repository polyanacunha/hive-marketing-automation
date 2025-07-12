using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Application.DTOs
{
    public record TargetingStrategy
    {
        [JsonPropertyName("audience")]
        public Audience Audience { get; set; }

        [JsonPropertyName("placements_suggestion")]
        public List<string> PlacementsSuggestion { get; set; }

        [JsonPropertyName("schedule_suggestion")]
        public ScheduleSuggestion ScheduleSuggestion { get; set; }
    }

    public record Audience
    {
        [JsonPropertyName("min_age")]
        public int MinAge { get; set; }

        [JsonPropertyName("max_age")]
        public int MaxAge { get; set; }

        [JsonPropertyName("genders")]
        public List<string> Genders { get; set; }

        [JsonPropertyName("interests")]
        public List<string> Interests { get; set; }

        [JsonPropertyName("locations")]
        public List<string> Locations { get; set; }
    }

    public record ScheduleSuggestion
    {
        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("daily_hours")]
        public string DailyHours { get; set; }
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

    public record CampaignStrategy
    {
        [JsonPropertyName("targeting_strategy")]
        public TargetingStrategy TargetingStrategy { get; set; }

        [JsonPropertyName("creative_suggestions")]
        public CreativeSuggestions CreativeSuggestions { get; set; }

        [JsonPropertyName("strategy_justification")]
        public string StrategyJustification { get; set; }
    }

}

