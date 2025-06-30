using Hive.Application.DTOs;
using Hive.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Interfaces
{
    public interface ITextGenerationService
    {
        Task<Result<string>> GenerateText(string promptSystem, string promptUser);
    }
}
