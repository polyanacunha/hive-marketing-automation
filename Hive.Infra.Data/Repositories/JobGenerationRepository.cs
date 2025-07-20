using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Repositories
{
    public class JobGenerationRepository : IJobGenerationRepository
    {
        private readonly ApplicationDbContext _context;

        public JobGenerationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobGeneration> Create(JobGeneration jobGeneration)
        {
            await _context.JobGeneration.AddAsync(jobGeneration);
            return jobGeneration;
        }

        public async Task<JobGeneration?> GetById(Guid id)
        {
            return await _context.JobGeneration
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
