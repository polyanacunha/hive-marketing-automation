using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class ImageUrl : Entity
    {
        public string ImageKey { get; private set; }
        public string ClientProfileId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public virtual ICollection<MidiaProduction> MidiaProductions { get; private set; } = new List<MidiaProduction>();


        public ImageUrl(string clientProfileId, string imageKey)
        {
            ClientProfileId = clientProfileId;
            ImageKey = imageKey;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
