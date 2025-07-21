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
    public class MidiaProductionRepository : IMidiaProductionRepository
    {
        private readonly ApplicationDbContext _context;

        public MidiaProductionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MidiaProduction> Create(MidiaProduction midiaProduction)
        {
            await _context.MidiaProduction.AddAsync(midiaProduction);
            return midiaProduction;
        }

        public async Task<MidiaProduction?> GetById(int id)
        {
            return await _context.MidiaProduction
                .Include(c=> c.InputImageUrl)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<MidiaProduction?> GetByIdWithJobsAsync(int id)
        {
            return await _context.MidiaProduction
                .Include(midiaProduction => midiaProduction.JobsGenerations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
