using Hive.Application.DTOs;
using Hive.Domain.Enum;
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
        ObjectiveCampaignEnum ObjectiveCampaign,
        string? PageId
        ) : IRequest<Result<CampaignStrategy>>
    {
    }
}
