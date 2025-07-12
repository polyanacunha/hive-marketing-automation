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
    public class ObjectiveCampaignRepository : IObjectiveCampaignRepository
    {
        private readonly ApplicationDbContext _context;

        public ObjectiveCampaignRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ObjectiveCampaign>> GetAll()
        {
            return await _context.ObjectiveCampaigns.OrderBy(c => c.Description).ToListAsync();
        }

        public async Task<ObjectiveCampaign?> GetById(int id)
        {
            return await _context.ObjectiveCampaigns
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
