using Hive.Application.DTOs.Meta;
using Hive.Application.DTOs;
using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hive.Domain.Enum;

namespace Hive.Application.Interfaces
{
    public interface IAdSetConfigStrategy
    {
        ObjectiveCampaignEnum Objective { get; }
        MetaAdSetConfig BuildAdSetAsync(CampaignStrategy campaignStrategy, string campaignId, MetaGeoLocationsConfig location, List<MetaInterestConfig> interests);
    }
}
