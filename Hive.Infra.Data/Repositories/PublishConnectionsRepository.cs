using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Enum;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace Hive.Infra.Data.Repositories
{
    public class PublishConnectionsRepository : IPublishConnectionRepository
    {
        private readonly ApplicationDbContext _context;

        public PublishConnectionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PublishConnection> Create(PublishConnection publishConnection)
        {
            await _context.PublishConnections.AddAsync(publishConnection);
            return publishConnection;
        }

        public async Task<PublishConnection?> GetMetaByClient(string id)
        {
            return await _context.PublishConnections
            .Where(p => p.ClientProfileId == id)
            .FirstOrDefaultAsync(p => p.Platform == Platform.Meta);

        }
    }
}
