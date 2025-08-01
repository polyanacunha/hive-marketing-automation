using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Application.DTOs.Meta
{
    public class AudienceConfig
    {
        [JsonPropertyName("age_min")]
        public int MinAge { get; set; }

        [JsonPropertyName("age_max")]
        public int MaxAge { get; set; }

        [JsonPropertyName("genders")]
        public List<int> Genders { get; set; }

        [JsonPropertyName("interests")]
        public List<Interest> Interests { get; set; }
        
        [JsonPropertyName("geo_locations")]
        public LocationsMeta GeoLocations { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UserOs { get; set; }
    }

    public class Interest 
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class LocationsMeta
    {
        [JsonPropertyName("countries")]
        public List<string> Countries { get; set; }

        [JsonPropertyName("regions")]
        public List<RegionMeta> Regions { get; set; }

        [JsonPropertyName("custom_locations")]
        public List<CustomLocations>? CustomLocations { get; set; }

    }

    public record RegionMeta
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class CustomLocations
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("radius")]
        public int Radius { get; set; }

        [JsonPropertyName("distance_unit")]
        public string DistanceUnit { get; set; }
    }
}
