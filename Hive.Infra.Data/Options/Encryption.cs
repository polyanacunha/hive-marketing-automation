using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Options
{
    public class Encryption
    {
        public const string EncryptionKey = "Encryption";
        public string Key { get; set; }
        public string IV { get; set; }
    }
}
