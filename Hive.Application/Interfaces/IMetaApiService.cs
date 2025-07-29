using Hive.Application.DTOs.Meta;
using Hive.Application.UseCases.Campaigns.CreateCampaign;
using Hive.Domain.Validation;

namespace Hive.Application.Interfaces
{
    public interface IMetaApiService
    {
        Task<Result<string>> GetUrlRedirect();
        Task<Result<string>> GetMetaAccessToken(string Code);
        Task<Result<string>> GetInfoUser(string AccessToken);
        Task<Result<string>> GetAllPages(string AccessToken);
        Task<Result<List<Interest>>> SearchInterests(List<string> Interest, string AccessToken);
        Task<Result<List<MetaInterestConfig>>> SearchInterestsBatch(List<string> interests, string accessToken);
        Task<Result<string>> SearchRegion(string RegionName, string CountryCode, string AccessToken);


        Task<Result<string>> CreateCampaign(MetaCampaignConfig content, string AccessToken);
        Task<Result<string>> CreateAdSet(MetaAdSetConfig content, string AccessToken);
    }
}
