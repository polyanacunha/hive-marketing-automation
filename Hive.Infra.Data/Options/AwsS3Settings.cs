using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Options
{
    public class AwsS3Settings
    {
        public const string AwsS3SettingsKey = "AwsS3Settings";
        public string BucketName { get; set; }
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
