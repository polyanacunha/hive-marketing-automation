using System.Text.Json.Serialization;

namespace Hive.Application.DTOs
{
    public record ScriptDto
    {
        [JsonPropertyName("script")]
        public List<SceneDto> Script { get; init; }
    };
}
