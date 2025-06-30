using Hive.Domain.Entities;


namespace Hive.Domain.Interfaces
{
    public interface IMidiaProductionRepository
    {
        Task<MidiaProduction> Create(MidiaProduction midiaProduction);
        Task<MidiaProduction?> GetById(int id);
        Task<MidiaProduction?> GetByIdWithJobsAsync(int id);
    }
}
