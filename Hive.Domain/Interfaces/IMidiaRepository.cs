using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hive.Domain.Entities;

namespace Hive.Domain.Interfaces;

public interface IMidiaRepository
{
    Task<IEnumerable<Midia>> GetMidiasAsync();
    Task<Midia> CreateAsync(Midia midia);
    Task<Midia> UpdateAsync(Midia midia);
    Task<Midia> DeleteAsync(int id);
    Task<Midia> GetByIdAsync(int id);
}
