using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Infra.Data.NovaPasta
{
    public record OpenAiChatResponse
    {
        [JsonPropertyName("choices")]
        public List<OpenAiChoice> Choices { get; init; }
    }

    public record OpenAiChoice
    {
        [JsonPropertyName("message")]
        public OpenAiChatMessage Message { get; init; }
    }

    public record OpenAiChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; init; }

        [JsonPropertyName("content")]
        public string Content { get; init; }
    }
}
