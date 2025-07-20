using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Client.CreateProfileClient
{
    public class ClientProfileCommandHandler : IRequestHandler<CreateClientProfileCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientProfileRepository _clientProfileRepository;
        private readonly ITargetAudienceRepository _targetAudienceRepository;
        private readonly IMarketSegmentRepository _marketSegmentRepository;
        private readonly ICurrentUser _currentUser;

        public ClientProfileCommandHandler(IUnitOfWork unitOfWork, IClientProfileRepository clientProfileRepository, ITargetAudienceRepository targetAudienceRepository, IMarketSegmentRepository marketSegmentRepository, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _clientProfileRepository = clientProfileRepository;
            _targetAudienceRepository = targetAudienceRepository;
            _marketSegmentRepository = marketSegmentRepository;
            _currentUser = currentUser;
        }
 
        public async Task<Result<Unit>> Handle(CreateClientProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId
                ?? throw new DomainExceptionValidation("User not authenticated or invalid token.");

            var marketSegment = await _marketSegmentRepository.GetById(request.MarketSegmentId)
                ?? throw new DomainExceptionValidation("invalid market segment.");

            var targetAudience = await _targetAudienceRepository.GetById(request.TargetAudienceId)
                ?? throw new DomainExceptionValidation("invalid target audience.");

            var clientProfile = new ClientProfile
            (
                id: userId,
                companyName: request.CompanyName,
                targetAudience: targetAudience,
                targetAudienceId: targetAudience.Id,
                marketSegment: marketSegment,
                marketSegmentId: marketSegment.Id,
                taxId: request.TaxId,
                webSiteUrl: request.WebSiteUrl
            );

            await _clientProfileRepository.Create(clientProfile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result<Unit>.Success(Unit.Value);
        }
    }
}