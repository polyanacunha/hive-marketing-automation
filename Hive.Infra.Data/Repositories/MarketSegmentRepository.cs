using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hive.Infra.Data.Repositories
{
    public class MarketSegmentRepository : IMarketSegmentRepository
    {
        private readonly ApplicationDbContext _context;

        public MarketSegmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MarketSegment> Create(MarketSegment marketSegment)
        {
            await _context.MarketSegment.AddAsync(marketSegment);
            return marketSegment;
        }

        public async Task<MarketSegment?> GetById(int Id)
        {
            return await _context.MarketSegment
            .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<IEnumerable<MarketSegment>> GetAll()
        {
            return await _context.MarketSegment.OrderBy(c => c.Description).ToListAsync();
        }
    }
}