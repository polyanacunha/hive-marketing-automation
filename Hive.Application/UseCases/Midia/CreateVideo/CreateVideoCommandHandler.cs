using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Midia.CreateVideo
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, Result<int>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IClientProfileRepository _clientProfileRepository;
        private readonly IImageUrlRepository _imageUrlRepository;
        private readonly IPromptProcessor _promptVideoProcessor;
        private readonly IMidiaProductionRepository _midiaProductionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundScheduler _backgroundScheduler;

        public CreateVideoCommandHandler(ICurrentUser currentUser, IClientProfileRepository clientProfileRepository, IPromptProcessor promptProcessor, IMidiaProductionRepository midiaProductionRepository, IUnitOfWork unitOfWork, IBackgroundScheduler backgroundJobService, IImageUrlRepository imageUrlRepository)
        {
            _currentUser = currentUser;
            _clientProfileRepository = clientProfileRepository;
            _promptVideoProcessor = promptProcessor;
            _midiaProductionRepository = midiaProductionRepository;
            _unitOfWork = unitOfWork;
            _backgroundScheduler = backgroundJobService;
            _imageUrlRepository = imageUrlRepository;
        }

        public async Task<Result<int>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            var client = await _clientProfileRepository.GetById(clientId!);

            if (client == null)
            {
                return Result<int>.Failure("Client profile not found.");
            }

            var images = await _imageUrlRepository.GetByIdsAndClientAsync(request.InputImagesId, client.Id);

       
            if (images.Count != request.InputImagesId.Count)
            {
                return Result<int>.Failure("One or more images were not discovered or do not belong to you.");
            }

            var (promptSystem,  promptUser ) = await _promptVideoProcessor.PromptToCreateVideo(client, request.ClientObservations);

            var midia = new MidiaProduction(
                clientProfileId: client.Id,
                systemPrompt: promptSystem,
                userPrompt: promptUser,
                inputImages: images
                );

            await _midiaProductionRepository.Create(midia);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _backgroundScheduler.ScheduleScriptGeneration(midia.Id);

            return Result<int>.Success(midia.Id);

        }
    }
}
