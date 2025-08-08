using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Domain.Enum;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Campaigns.Meta.CreateCampaign
{
    public record CreateCampaignCommand(
        string CampaignName, 
        string ProductDescription,
        int Budget, 
        DateTime InitialDate, 
        DateTime EndDate, 
        ObjectiveCampaignEnum ObjectiveCampaign,
        Platform Platform,
        MetaParamerters? MetaConfig
        ) : IRequest<Result<CampaignStrategy>>
    {
    }
}
