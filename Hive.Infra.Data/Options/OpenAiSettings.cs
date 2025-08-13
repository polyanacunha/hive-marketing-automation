using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Options
{
    public class OpenAiSettings
    {
        public const string OpenAiSettingsKey = "OpenAI";
        public string ApiKey { get; set; }
        public string ChatGptModel { get; set; }
    }
}
