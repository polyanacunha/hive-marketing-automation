using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Domain.Entities;

namespace Hive.Domain.Interfaces
{
    public interface IMarketSegmentRepository
    {
        Task<MarketSegment> Create(MarketSegment marketSegment);
        Task<MarketSegment?> GetById(int Id);
        Task<IEnumerable<MarketSegment>> GetAll();
    }
}