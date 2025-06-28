using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hive.Infra.Data.Repositories
{
    public class TargetAudienceRepository : ITargetAudienceRepository
    {
        private readonly ApplicationDbContext _context;

        public TargetAudienceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TargetAudience> Create(TargetAudience targetAudience)
        {
            await _context.TargetAudience.AddAsync(targetAudience);
            return targetAudience;
        }

        public async Task<TargetAudience?> GetById(int id)
        {
            return await _context.TargetAudience
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<TargetAudience>> GetAll()
        {
            return await _context.TargetAudience.OrderBy(c => c.Description).ToListAsync();
        }
    }
}