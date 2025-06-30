using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Midia.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Result<List<UploadedImageDto>>>
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

        public async Task<Result<List<UploadedImageDto>>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            if (clientId == null)
            {
                return Result<List<UploadedImageDto>>.Failure("User are not authenticated");
            }

            var client = await _clientProfileRepository.GetById(clientId.Value);

            if (client == null)
            {
                return Result<List<UploadedImageDto>>.Failure("Client profile not found.");
            }
            var images = request.Files.Select(file => 
                new SaveImage(
                    client.Id.ToString(), 
                    file.OpenReadStream(),
                    file.FileName, 
                    request.AlbumName
                    ))
                .ToList();

            var imagesUrl = await _storageService.SaveFileAsync(images);

            var newImagesEntity = new List<ImageUrl>();

            foreach (var url in imagesUrl)
            {
                var imageEntity = new ImageUrl(url, clientId.Value);
                newImagesEntity.Add(imageEntity);
            }

            await _imageUrlRepository.CreateRangeAsync(newImagesEntity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseDtos = newImagesEntity
                .Select(entity => new UploadedImageDto(entity.Id, entity.Url))
                .ToList();

            return Result<List<UploadedImageDto>>.Success(responseDtos);

        }
    }
}
