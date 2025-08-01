using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Enum;
using Hive.Domain.Validation;
using MediatR;
using System.Text.Json;

namespace Hive.Application.UseCases.Campaigns.Meta.SaveMetaAccessToken
{
    public class SaveMetaAccessTokenCommandHandler : IRequestHandler<SaveMetaAccessTokenCommand, Result<string>>
    {
        private readonly IMetaApiService _metaApiService;
        private readonly IPublishConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public SaveMetaAccessTokenCommandHandler(IMetaApiService metaApiService, IPublishConnectionRepository connectionRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _metaApiService = metaApiService;
            _connectionRepository = connectionRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(SaveMetaAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            var userToken = await _connectionRepository.GetMetaByClient(clientId!);

            var tokenJson = await _metaApiService.GetMetaAccessToken(request.Code);

            if (tokenJson.IsFailure)
            {
                return Result<string>.Failure("Erro ao obter token de acesso.");
            }

            var token = JsonSerializer.Deserialize<FacebookToken>(tokenJson.Value!);

            if (token == null)
            {
                throw new Exception("Erro ao desserializar json");
            }

            if (userToken == null)
            {
                var publishConnection = new PublishConnection
                    (
                        clientId!,
                        Platform.Meta,
                        DateTime.UtcNow.AddSeconds(token!.ExpiresIn),
                        token.AccessToken
                    );

                await _connectionRepository.Create(publishConnection);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<string>.Success("Ok");
            }

            userToken.SetAccessToken(token.AccessToken);
            userToken.UpdateExpiresDate(DateTime.UtcNow.AddSeconds(token.ExpiresIn));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Ok");

        }
    }
}
