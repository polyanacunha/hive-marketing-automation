using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Interfaces
{
    public interface IObjectiveCampaignRepository
    {
        Task<IEnumerable<ObjectiveCampaign>> GetAll();
        Task<ObjectiveCampaign?> GetById(int id);
    }
}
