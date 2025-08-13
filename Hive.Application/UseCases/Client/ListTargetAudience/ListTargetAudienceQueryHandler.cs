using AutoMapper.Configuration.Annotations;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Client.ListTargetAudience
{
    public class ListTargetAudienceQueryHandler : IRequestHandler<ListTargetAudienceQuery, Result<List<TargetAudience>>>
    {

        private readonly ITargetAudienceRepository _targetAudienceRepository;

        public ListTargetAudienceQueryHandler(ITargetAudienceRepository targetAudienceRepository)
        {
            _targetAudienceRepository = targetAudienceRepository;
        }

        public async Task<Result<List<TargetAudience>>> Handle(ListTargetAudienceQuery request, CancellationToken cancellationToken)
        {
            var list  = await _targetAudienceRepository.GetAll();

            if (list == null) 
            {
                return Result<List<TargetAudience>>.Failure("Não foram encontrados públicos alvos.");
            }

            return Result<List<TargetAudience>>.Success(list.ToList());
        }
    }
}
