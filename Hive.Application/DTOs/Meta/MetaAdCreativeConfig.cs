using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Application.DTOs.Meta
{
    public class MetaAdConfig
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("adset_id")]
        public string AdSetId { get; set; } // Será preenchido após a criação do Ad Set

        [JsonPropertyName("status")]
        public string Status { get; set; } = "PAUSED";

        [JsonPropertyName("creative")]
        public MetaCreativeConfig Creative { get; set; }
    }

    public class MetaCreativeConfig
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("object_story_spec")]
        public MetaObjectStorySpec ObjectStorySpec { get; set; }
    }

    public class MetaObjectStorySpec
    {
        [JsonPropertyName("page_id")]
        public string PageId { get; set; }

        [JsonPropertyName("link_data")]
        public MetaLinkData LinkData { get; set; }
    }

    public class MetaLinkData
    {
        [JsonPropertyName("link")]
        public string Link { get; set; } // A URL de destino final (request.WebsiteUrl)

        [JsonPropertyName("message")]
        public string Message { get; set; } // Texto principal do anúncio

        [JsonPropertyName("name")]
        public string? Name { get; set; } // Título (Headline) do anúncio

        [JsonPropertyName("image_hash")]
        public string ImageHash { get; set; } // Hash da imagem já enviada para a Meta

        [JsonPropertyName("call_to_action")]
        public MetaCallToAction CallToAction { get; set; }
    }

    public class MetaCallToAction
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "LEARN_MORE"; // Ex: SAIBA_MAIS
    }
}
