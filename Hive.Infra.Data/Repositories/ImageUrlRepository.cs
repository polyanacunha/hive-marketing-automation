using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace Hive.Infra.Data.Repositories
{
    public class ImageUrlRepository : IImageUrlRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageUrlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ImageUrl> Create(ImageUrl imageUrl)
        {
            await _context.ImageUrl.AddAsync(imageUrl);
            return imageUrl;
        }

        public async Task CreateRangeAsync(IEnumerable<ImageUrl> imageUrls)
        {
            await _context.ImageUrl.AddRangeAsync(imageUrls);
        }

        public async Task<IEnumerable<ImageUrl>> GetAllByClient(string clientId)
        {
            return await _context.ImageUrl                                 
            .Where(imageUrl => imageUrl.ClientProfileId == clientId)                                         
            .ToListAsync();
        }

        public async Task<ImageUrl?> GetById(int id)
        {
            return await _context.ImageUrl
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ImageUrl>> GetByIdsAndClientAsync(IEnumerable<int> imageUrlIds, string clientId)
        {
            var idsToSearch = imageUrlIds.ToList();

            if (!idsToSearch.Any())
            {
                return new List<ImageUrl>();
            }

            return await _context.ImageUrl
                .Where(img => img.ClientProfileId == clientId)
                .Where(img => idsToSearch.Contains(img.Id))
                .ToListAsync();
        }
    }
}
