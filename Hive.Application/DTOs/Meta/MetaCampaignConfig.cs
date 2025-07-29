using System.Text.Json.Serialization;

namespace Hive.Application.DTOs.Meta
{
    public class MetaCampaignConfig
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("objective")]
        public string Objective { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "PAUSED";

        [JsonPropertyName("special_ad_categories")]
        public List<string> SpecialAdCategories { get; set; } = new List<string> { "NONE" };
    }
}
