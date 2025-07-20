using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Midia.GetImages
{
    public class GetAlImagesQueryHandler : IRequestHandler<GetAllImagesQuery, Result<List<ImageResponse>>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IImageUrlRepository _urlRepository;
        private readonly IStorageService _storageService;

        public GetAlImagesQueryHandler(ICurrentUser currentUser, IImageUrlRepository urlRepository, IStorageService storageService)
        {
            _currentUser = currentUser;
            _urlRepository = urlRepository;
            _storageService = storageService;
        }

        public async Task<Result<List<ImageResponse>>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            if (clientId == null)
            {
                return Result<List<ImageResponse>>.Failure("User are not authenticated");
            }

            var images = await _urlRepository.GetAllByClient(clientId);

            if (images == null)
            {
                return Result<List<ImageResponse>>.Failure("Nao ha imagens");
            }

            var result = new List<ImageResponse>();

            foreach (var img in images)
            {
                var url = await _storageService.GetFileFileAsync(img.ImageKey, 15);
                result.Add(new ImageResponse(img.Id, url));
            }

            return Result<List<ImageResponse>>.Success(result);
        }
    }
}
