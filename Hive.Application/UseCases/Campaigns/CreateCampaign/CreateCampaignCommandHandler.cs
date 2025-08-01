using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using Hive.Domain.Entities;
using Hive.Application.Interfaces;
using Hive.Application.DTOs;


namespace Hive.Application.UseCases.Campaigns.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Result<CampaignStrategy>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IPromptProcessor _promptProcessor;
        private readonly IClientProfileRepository _clientProfileRepository;
        private readonly IPublishConnectionRepository _connectionRepository;
        private readonly ITextGenerationService _textGenerationService;
        private readonly IMetaCampaignStrategyMapper _campaignMapper;

        private readonly IMetaApiService _metaApiService;


        public CreateCampaignCommandHandler(ICurrentUser currentUser, IPromptProcessor promptProcessor, IClientProfileRepository clientProfileRepository, ITextGenerationService textGenerationService, IMetaApiService metaApiService, IPublishConnectionRepository connectionRepository, IMetaCampaignStrategyMapper campaignMapper)
        {
            _currentUser = currentUser;
            _promptProcessor = promptProcessor;
            _clientProfileRepository = clientProfileRepository;
            _textGenerationService = textGenerationService;
            _metaApiService = metaApiService;
            _connectionRepository = connectionRepository;
            _campaignMapper = campaignMapper;
        }

        public async Task<Result<CampaignStrategy>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            var client = await _clientProfileRepository.GetById(clientId!);

            if (client == null)
            {
                return Result<CampaignStrategy>.Failure("Informacões da empresa não preenchidas.");
            }

            var userToken = await _connectionRepository.GetMetaByClient(clientId!);

            if (userToken == null)
            {
                return Result<CampaignStrategy>.Failure("Conta Meta não vinculada.");
            }

            var campaign = new Campaign
            (
                clientProfileId: clientId!,
                campaignName: request.CampaignName,
                produtoDescription: request.ProductDescription,
                objectiveCampaign: request.ObjectiveCampaign,
                budget: request.Budget,
                initial:request.InitialDate,
                end: request.EndDate
            );

            var (promptSystem, promptUser) = await _promptProcessor.PromptToGenerateTargeting(client, campaign);

            var targetingJson = await _textGenerationService.GenerateText(promptSystem, promptUser);

            if (targetingJson.IsFailure)
            {
                return Result<CampaignStrategy>.Failure("Erro ao gerar estratégia de campanha.");
            }

            var targeting = await _promptProcessor.DeserializeJson<CampaignStrategy>(targetingJson.Value!);

            targeting.Campaign = campaign;

            var campaignMeta = _campaignMapper.CreateMetaCampaign(targeting, userToken.AccessToken, request.AccountIdMeta);

            return Result<CampaignStrategy>.Success(targeting);
        }
    }
}
