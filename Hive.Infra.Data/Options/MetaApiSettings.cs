using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Options
{
    public class MetaApiSettings
    {
        public const string MetaApiSettingsKey = "MetaApiSettings";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string FacebookUrlBase { get; set; }
        public string MarketingApiUrlBase { get; set; }
        public string Scopes { get; set; }
        public string RedirectUri { get; set; }
    }
}
