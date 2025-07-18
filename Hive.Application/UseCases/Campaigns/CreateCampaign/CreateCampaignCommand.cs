using Hive.Application.DTOs;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Campaigns.CreateCampaign
{
    public record CreateCampaignCommand(
        string CampaignName, 
        string ProductDescription,
        int Budget, 
        DateTime InitialDate, 
        DateTime EndDate, 
        int ObjectiveCampaignId) : IRequest<Result<CampaignStrategy>>
    {
    }
}
