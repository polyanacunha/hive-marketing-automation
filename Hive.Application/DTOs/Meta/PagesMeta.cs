using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hive.Application.DTOs.Meta
{
    public record PagesMeta
    {
        public string AccountId { get; set; }

        public string Name { get; set; }

        public string UrlPicture { get; set; }

    }
}
