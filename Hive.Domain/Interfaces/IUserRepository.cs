using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<string?> GetByRefrashTokenAsync();
        Task<UserAccount?> Create(UserAccount user);
    }
}
