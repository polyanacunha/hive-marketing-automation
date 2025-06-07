using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hive.Infra.Data.Repositories;

public class MidiaRepository : IMidiaRepository
{
    private ApplicationDbContext _midiaContext;
    public MidiaRepository(ApplicationDbContext context)
    {
        _midiaContext = context;
    }

    public async Task<Midia> CreateAsync(Midia midia)
    {
        _midiaContext.Add(midia);
        await _midiaContext.SaveChangesAsync();
        return midia;
    }

    public async Task<IEnumerable<Midia>> GetMidiasAsync()
    {
        return await _midiaContext.Midia.ToListAsync();
    }

    public async Task<Midia> DeleteAsync(int id)
    {
        _midiaContext.Remove(id);
        await _midiaContext.SaveChangesAsync();
        return null;
    }

    public async Task<Midia> UpdateAsync(Midia midia)
    {
        _midiaContext.Update(midia);
        await _midiaContext.SaveChangesAsync();
        return midia;
    }

    public async Task<Midia> GetByIdAsync(int id) 
    {
        var midia = await _midiaContext.FindAsync<Midia>(id);
        return midia;

    }
}
