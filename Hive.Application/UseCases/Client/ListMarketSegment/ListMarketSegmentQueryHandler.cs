using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Client.ListMarketSegment
{
    public class ListMarketSegmentQueryHandler : IRequestHandler<ListMarketSegmentQuery, Result<List<MarketSegment>>>
    {
        private readonly IMarketSegmentRepository _marketSegmentRepository;

        public ListMarketSegmentQueryHandler(IMarketSegmentRepository marketSegmentRepository)
        {
            _marketSegmentRepository = marketSegmentRepository;
        }

        public async Task<Result<List<MarketSegment>>> Handle(ListMarketSegmentQuery request, CancellationToken cancellationToken)
        {
            var list = await _marketSegmentRepository.GetAll();

            if (list == null)
            {
                return Result<List<MarketSegment>>.Failure("Não foram encontrados segmentos de mercado.");
            }

            return Result<List<MarketSegment>>.Success(list.ToList());
        }
    }
}
