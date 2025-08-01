using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Dtos.Meta
{
    public record MetaApiErrorResponse
    {
        [JsonPropertyName("error")]
        public MetaError Error { get; init; }
    }

    public record MetaError
    {
        [JsonPropertyName("message")]
        public string Message { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("code")]
        public int Code { get; init; }

        [JsonPropertyName("error_subcode")]
        public int? ErrorSubcode { get; init; }

        [JsonPropertyName("fbtrace_id")]
        public string FbTraceId { get; init; }
    }
}
