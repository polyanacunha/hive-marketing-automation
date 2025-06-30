using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class ImageUrl : Entity
    {
        public string Url { get; private set; }
        public Guid ClientProfileId { get; private set; }
        public virtual ICollection<MidiaProduction> MidiaProductions { get; private set; } = new List<MidiaProduction>();


        public ImageUrl(string url, Guid clientProfileId)
        {
            Url = url;
            ClientProfileId = clientProfileId;
        }
    }
}
