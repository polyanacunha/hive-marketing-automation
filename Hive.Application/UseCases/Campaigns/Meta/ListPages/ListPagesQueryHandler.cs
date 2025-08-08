using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Campaigns.Meta.ListPages
{
    public class ListPagesQueryHandler : IRequestHandler<ListPagesQuery, Result<List<PagesMeta>>>
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

        public async Task<Result<List<PagesMeta>>> Handle(ListPagesQuery request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            var userToken = await _connectionRepository.GetMetaByClient(clientId!);

            if (userToken == null)
            {
                return Result<List<PagesMeta>>.Failure("Conta de anunciante Meta não vinculada.");
            }

            var pages = await _metaApiService.GetAllPages(userToken.AccessToken);

            if(pages.IsFailure)
            {
                return Result<List<PagesMeta>>.Failure(pages.Errors);
            }

            return Result<List<PagesMeta>>.Success(pages.Value!);
        }
    }
}
