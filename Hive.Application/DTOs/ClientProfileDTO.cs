using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.DTOs
{
    public record ClientProfileDTO (int MarketSegmentId, int TargetAudienceId, string CompanyName, string? WebSiteUrl, string TaxId){}
}
