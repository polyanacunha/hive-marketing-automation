using Hive.Domain.Entities;


namespace Hive.Application.Interfaces
{
    public interface IPromptVideoProcessor
    {
        Task<(string promptSystem, string promptUser)> ContextualizePromptToCreateVideo(ClientProfile client, string clientObservations);
    }
}
