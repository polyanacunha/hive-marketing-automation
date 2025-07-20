using System.Text.Json.Serialization;


namespace Hive.Application.DTOs
{
    public record SceneDto
    {
        [JsonPropertyName("scene_number")]
        public int SceneNumber { get; init; }

        [JsonPropertyName("duration_seconds")]
        public int DurationSeconds { get; init; }

        [JsonPropertyName("visual_prompt")]
        public string VisualPrompt { get; init; }

        [JsonPropertyName("on_screen_text")]
        public string OnScreenText { get; init; }

        [JsonPropertyName("narrator_script")]
        public string NarratorScript { get; init; }
    }
}
