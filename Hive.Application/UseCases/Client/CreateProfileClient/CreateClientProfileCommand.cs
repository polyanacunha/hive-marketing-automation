using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Client
{
    public record CreateClientProfileCommand(
        Guid UserId,
        int MarketSegmentId, 
        int TargetAudienceId, 
        string CompanyName, 
        string? WebSiteUrl, 
        string TaxId) : IRequest<Result<Unit>>
    {}
}