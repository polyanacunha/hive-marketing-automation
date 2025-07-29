using Hive.Application.Interfaces;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Campaigns.ListPages
{
    public class ListPagesQueryHandler : IRequestHandler<ListPagesQuery, Result<string>>
    {
        private readonly IMetaApiService _metaApiService;
        private readonly ICurrentUser _currentUser;
        private readonly IPublishConnectionRepository _connectionRepository;

        public ListPagesQueryHandler(IMetaApiService metaApiService, ICurrentUser currentUser, IPublishConnectionRepository connectionRepository)
        {
            _metaApiService = metaApiService;
            _currentUser = currentUser;
            _connectionRepository = connectionRepository;
        }

        public async Task<Result<string>> Handle(ListPagesQuery request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            var userToken = await _connectionRepository.GetMetaByClient(clientId!);

            if (userToken == null)
            {
                return Result<string>.Failure("Conta de anunciante Meta não vinculada.");
            }

            var pagesJson = await _metaApiService.GetAllPages(userToken.AccessToken);

            if(pagesJson.IsFailure)
            {
                return Result<string>.Failure(pagesJson.Errors);
            }

            return Result<string>.Success(pagesJson.Value!);
        }
    }
}
