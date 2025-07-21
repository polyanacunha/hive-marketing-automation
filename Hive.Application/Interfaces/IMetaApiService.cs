using Hive.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IMetaApiService
    {
        Task<Result<string>> GetUrlRedirect();
        Task<Result<string>> GetMetaAccessToken(string Code);
    }
}
