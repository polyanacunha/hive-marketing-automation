using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Application.DTOs.Meta
{
    public record AdAccount
    {
        [JsonPropertyName("id")]
        public string AccountId  { get; set; }

        [JsonPropertyName("name")]
        public string Name  { get; set; }

    }
}
