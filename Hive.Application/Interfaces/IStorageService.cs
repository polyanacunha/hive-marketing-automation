using Hive.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface IStorageService
    {
        Task<List<string>> SaveFileAsync(List<SaveImage> images);
    }
}
