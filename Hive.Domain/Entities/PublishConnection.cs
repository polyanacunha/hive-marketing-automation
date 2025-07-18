using Hive.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class PublishConnection : Entity
    {
        public string ClientProfileId { get; private set; }
        public Platform Platform { get; private set; }
        public DateTime Expires { get; private set; }
        public string AccessToken { get; private set; }

        public PublishConnection(string clientProfileId, Platform platform, DateTime expires, string accessToken)
        {
            ClientProfileId = clientProfileId;
            Platform = platform;
            Expires = expires;
            AccessToken = accessToken;
        }
    }
}
