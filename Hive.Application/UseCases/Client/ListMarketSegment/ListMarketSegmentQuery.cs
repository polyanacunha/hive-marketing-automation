using Hive.Domain.Entities;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Client.ListMarketSegment
{
    public record ListMarketSegmentQuery : IRequest<Result<List<MarketSegment>>>
    {
    }
}
