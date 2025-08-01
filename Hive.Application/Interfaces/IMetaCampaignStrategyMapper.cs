using Hive.Application.DTOs.Meta;
using Hive.Application.DTOs;
using Hive.Domain.Entities;
using MediatR;


namespace Hive.Application.Interfaces
{
    public interface IMetaCampaignStrategyMapper
    {
        Task<string> CreateMetaCampaign(CampaignStrategy campaignStrategy, string accessToken, string AccountId);
    }
}
