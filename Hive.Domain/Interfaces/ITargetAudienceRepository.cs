using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Domain.Entities;

namespace Hive.Domain.Interfaces
{
    public interface ITargetAudienceRepository
    {
        Task<TargetAudience> Create(TargetAudience targetAudience);
        Task<TargetAudience?> GetById(int id);
        Task<IEnumerable<TargetAudience>> GetAll();
    }
}