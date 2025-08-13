using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Dtos.Meta
{
    public class PagesResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("picture")]
        public DataPicture Picture { get; set; }

    }

    public class DataPicture
    {
        [JsonPropertyName("data")]
        public Picture Data { get; set; }
    }

    public class Picture
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

}
