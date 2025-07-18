using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class MidiaLinks : Entity
    {
        public string Value { get; private set; }
        public string ClientProfileId { get; private set; }

        public MidiaLinks(string value, string clientProfileId)
        {
            Value = value;
            ClientProfileId = clientProfileId;
        }

        public MidiaLinks()
        {
        }
    }
}
