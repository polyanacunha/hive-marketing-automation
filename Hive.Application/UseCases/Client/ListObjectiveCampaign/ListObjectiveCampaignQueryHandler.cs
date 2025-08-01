using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Client.ListObjectiveCampaign
{
    public class ListObjectiveCampaignQueryHandler : IRequestHandler<ListObjectiveCampaignQuery, Result<List<ObjectiveCampaign>>>
    {
        private readonly IObjectiveCampaignRepository _repository;

        public ListObjectiveCampaignQueryHandler(IObjectiveCampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ObjectiveCampaign>>> Handle(ListObjectiveCampaignQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAll();

            if (list == null)
            {
                return Result<List<ObjectiveCampaign>>.Failure("Não foram encontrados obejtivos de campanhas.");
            }

            return Result<List<ObjectiveCampaign>>.Success(list.ToList());
        }
    }
}
