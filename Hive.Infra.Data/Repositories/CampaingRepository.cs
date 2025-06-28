 using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hive.Infra.Data.Repositories;

public class CampaingRepository : ICampaingRepository
{
    private ApplicationDbContext _campaingContext;
    public CampaingRepository(ApplicationDbContext context)
    {
        _campaingContext = context;
    }

    public async Task<Campaing> Create(Campaing campaing)
    {
        _campaingContext.Add(campaing);
        await _campaingContext.SaveChangesAsync();
        return campaing;
    }

    public async Task<Campaing> GetById(int? id)
    {
        var campaing = await _campaingContext.Campaing.FindAsync(id);
        return campaing;
    }

    public async Task<IEnumerable<Campaing>> GetCampaings()
    {
        return await _campaingContext.Campaing.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Campaing> Remove(Campaing campaing)
    {
        _campaingContext.Remove(campaing);
        await _campaingContext.SaveChangesAsync();
        return campaing;
    }

    public async Task<Campaing> Update(Campaing campaing)
    {
        _campaingContext.Update(campaing);
        await _campaingContext.SaveChangesAsync();
        return campaing;
    }
}
