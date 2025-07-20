using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hive.Domain.Interfaces
{
    public interface ICurrentUser
    {
        string? UserId { get; }
    }
}