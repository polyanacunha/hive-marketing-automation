using Hive.Domain.Entities;


namespace Hive.Domain.Interfaces
{
    public interface IJobGenerationRepository
    {
        Task<JobGeneration> Create(JobGeneration jobGeneration);
        Task<JobGeneration?> GetById(int id);
    }
}