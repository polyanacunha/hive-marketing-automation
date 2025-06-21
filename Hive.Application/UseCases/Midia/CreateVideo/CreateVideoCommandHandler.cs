using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Enum;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Midia.CreateVideo
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, Result<int>>
    {
        private readonly IStorageService _storageService;
        private readonly ICurrentUser _currentUser;
        private readonly IClientProfileRepository _clientProfileRepository;
        private readonly IPromptProcessor _promptProcessor;
        private readonly IJobGenerationRepository _jobGenerationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVideoCommandHandler(IStorageService storageService, ICurrentUser currentUser, IClientProfileRepository clientProfileRepository, IPromptProcessor promptProcessor, IJobGenerationRepository jobGenerationRepository, IUnitOfWork unitOfWork)
        {
            _storageService = storageService;
            _currentUser = currentUser;
            _clientProfileRepository = clientProfileRepository;
            _promptProcessor = promptProcessor;
            _jobGenerationRepository = jobGenerationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            if (clientId == null) {
                return Result<string>.Failure("User not found");
            }

            var client = await _clientProfileRepository.GetById(clientId.Value);

            if (client == null)
            {
                return Result<string>.Failure("Client profile not found.");
            }

            var imagesUrl = await _storageService.SaveFileAsync(request.Files, request.AlbumName);

            var prompt = await _promptProcessor.ContextualizePrompt(client);

            var job = new JobGeneration(
                clientProfileId: clientId.Value,
                clientProfile: client,
                prompt: prompt.Value!,
                inputImageUrl: imagesUrl,
                assetType: AssetType.VIDEO
                );

            await _jobGenerationRepository.Create(job);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(job.Id);

        }
    }
}
