using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Interfaces
{
    public interface IJobGenerationRepository
    {
        Task<JobGeneration> Create(JobGeneration jobGeneration);
        Task<JobGeneration?> GetById(int id);
    }
}
