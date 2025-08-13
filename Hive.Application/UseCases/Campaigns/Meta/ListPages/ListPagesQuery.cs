using Hive.Application.DTOs.Meta;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Campaigns.Meta.ListPages
{
    public record ListPagesQuery() : IRequest<Result<List<PagesMeta>>>
    {
    }
}
