using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Midia.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Result<Unit>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IClientProfileRepository _clientProfileRepository;
        private readonly IImageUrlRepository _imageUrlRepository;
        private readonly IStorageService _storageService;
        private readonly IUnitOfWork _unitOfWork;

        public UploadImageCommandHandler(ICurrentUser currentUser, IClientProfileRepository clientProfileRepository, IStorageService storageService, IUnitOfWork unitOfWork, IImageUrlRepository imageUrlRepository)
        {
            _currentUser = currentUser;
            _clientProfileRepository = clientProfileRepository;
            _storageService = storageService;
            _unitOfWork = unitOfWork;
            _imageUrlRepository = imageUrlRepository;
        }

        public async Task<Result<Unit>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            if (clientId == null)
            {
                return Result<Unit>.Failure("User are not authenticated");
            }

            var client = await _clientProfileRepository.GetById(clientId);

            if (client == null)
            {
                return Result<Unit>.Failure("Client profile not found.");
            }
        
            var newImagesEntity = new List<ImageUrl>();

            foreach (var file in request.Files)
            {
                var imageKey = await _storageService.SaveFileAsync(file.OpenReadStream(), file.FileName);
                var imageEntity = new ImageUrl(clientId,imageKey);
                newImagesEntity.Add(imageEntity);
            }

            await _imageUrlRepository.CreateRangeAsync(newImagesEntity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);

        }
    }
}
