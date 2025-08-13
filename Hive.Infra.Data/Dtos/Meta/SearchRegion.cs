using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Dtos.Meta
{
    public record SearchRegion
    {
        [JsonPropertyName("key")]
        public string Key { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; init; }
    }
}
