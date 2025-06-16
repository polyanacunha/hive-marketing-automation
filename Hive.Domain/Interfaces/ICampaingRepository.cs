using System.Collections.Generic;
using System.Threading.Tasks;
using Hive.Domain.Entities;

namespace Hive.Domain.Interfaces;

public interface ICampaingRepository
{
    Task<IEnumerable<Campaing>> GetCampaings();
    Task<Campaing> GetById(int? id);
    Task<Campaing> Create(Campaing campaing);
    Task<Campaing> Update(Campaing campaing);
    Task<Campaing> Remove(Campaing campaing);
}
