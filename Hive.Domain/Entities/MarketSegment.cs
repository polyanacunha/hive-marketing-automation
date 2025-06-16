using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Entities
{
    public class MarketSegment : Entity
    {
        public string Description { get; set; }

        public MarketSegment(string description)
        {
            Description = description;
        }
    }
}
