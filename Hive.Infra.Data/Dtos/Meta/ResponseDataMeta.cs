using System.Text.Json.Serialization;

namespace Hive.Infra.Data.Dtos.Meta
{
    public class ResponseDataMeta<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }
    }
}
