using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class ClientProfile : Entity
    {
        public string MarketSegment {  get; private set; }
        public string TargetAudience {  get; private set; }
        public int TargetAudienceId { get; private set; }
        public string CompanyName { get; private set; }
        public string OwnerName { get; private set; }
        public string TaxId { get; private set; }

    }
}
