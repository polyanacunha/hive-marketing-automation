using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Interfaces
{
    public interface IImageUrlRepository
    {
        Task<ImageUrl> Create(ImageUrl imageUrl);
        Task<ImageUrl?> GetById(int id);
        Task<IEnumerable<ImageUrl>> GetAllByClient(string clientId);
        Task<List<ImageUrl>> GetByIdsAndClientAsync(IEnumerable<int> imageUrlIds, string clientId);
        Task CreateRangeAsync(IEnumerable<ImageUrl> imageUrls);
    }
}
