using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Domain.Entities;

namespace Hive.Domain.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPosts();
    Task<Post> GetById(int? id);
    Task<Post> Create(Post campaing);
    Task<Post> Update(Post campaing);
    Task<Post> Remove(Post campaing);
}
