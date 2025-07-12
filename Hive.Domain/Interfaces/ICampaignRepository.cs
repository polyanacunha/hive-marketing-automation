using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Interfaces
{
    public interface ICampaignRepository
    {
        Task<Campaign> Create(Campaign campaign);
        Task<Campaign?> GetById(int id);
    }
}
