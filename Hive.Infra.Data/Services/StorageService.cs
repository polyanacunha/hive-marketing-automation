using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Microsoft.Extensions.Logging;


namespace Hive.Infra.Data.Services
{
    public class StorageService : IStorageService
    {
        private readonly ILogger<StorageService> _logger;

        public StorageService(ILogger<StorageService> logger)
        {
            _logger = logger;
        }

        public Task<List<string>> SaveFileAsync(List<SaveImage> images)
        {
            _logger.LogInformation("O servico de armazenamento foi chamado");

            var list = new List<string>();

            foreach (var item in images)
            {
                list.Add($"https://urlDeSalvar/{item.ClientId}/{item.AlbumName}/{item.FileName}");
            }

            return Task.FromResult(list);
        }
    }
}
