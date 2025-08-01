

namespace Hive.Application.Interfaces
{
    public interface IStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName);
        string GetFileFile(string key, int timeInMinutes);
    }
}
