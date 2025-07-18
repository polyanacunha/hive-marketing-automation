using Hive.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Interfaces
{
    public interface IPublishConnectionRepository
    {
        Task<PublishConnection> Create(PublishConnection publishConnection);
        Task<PublishConnection?> GetMetaByClient(string id);
    }
}
