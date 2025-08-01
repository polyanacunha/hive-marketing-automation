using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Application.UseCases.Campaigns.Meta.CreateCampaign.ListPages;
using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Campaigns.ListPages
{
    public class ListAdAccountsQueryHandler : IRequestHandler<ListAdAccountsQuery, Result<List<AdAccount>>>
    {
        private readonly IMetaApiService _metaApiService;
        private readonly ICurrentUser _currentUser;
        private readonly IPublishConnectionRepository _connectionRepository;

        public ListAdAccountsQueryHandler(IMetaApiService metaApiService, ICurrentUser currentUser, IPublishConnectionRepository connectionRepository)
        {
            _metaApiService = metaApiService;
            _currentUser = currentUser;
            _connectionRepository = connectionRepository;
        }

        public async Task<Result<List<AdAccount>>> Handle(ListAdAccountsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            var userToken = await _connectionRepository.GetMetaByClient(clientId!);

            if (userToken == null)
            {
                return Result<List<AdAccount>>.Failure("Conta de anunciante Meta não vinculada.");
            }

            var accounts = await _metaApiService.GetAllAdAccounts(userToken.AccessToken);

            if(accounts.IsFailure)
            {
                return Result<List<AdAccount>>.Failure(accounts.Errors);
            }

            return Result<List<AdAccount>>.Success(accounts.Value!);
        }
    }
}
