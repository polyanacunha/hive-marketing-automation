using Hive.Domain.Entities;


namespace Hive.Application.Interfaces
{
    public interface IPromptProcessor
    {
        Task<(string promptSystem, string promptUser)> PromptToCreateVideo(ClientProfile client, string clientObservations);
        Task<(string promptSystem, string promptUser)> PromptToGenerateTargeting(ClientProfile client, Campaign campaign);
        Task<T> DeserializeJson<T>(string json);
    }
}
