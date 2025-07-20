using Hive.Domain.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using Hive.Domain.Entities;
using Hive.Application.Interfaces;
using Hive.Application.DTOs;
using System.Text.Json;


namespace Hive.Application.UseCases.Campaigns.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Result<CampaignStrategy>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IObjectiveCampaignRepository _objectiveCampaignRepository;
        private readonly IPromptProcessor _promptProcessor;
        private readonly IClientProfileRepository _clientProfileRepository;
        private readonly ITextGenerationService _textGenerationService;


        public CreateCampaignCommandHandler(ICurrentUser currentUser, IObjectiveCampaignRepository objectiveCampaignRepository, IPromptProcessor promptProcessor, IClientProfileRepository clientProfileRepository, ITextGenerationService textGenerationService)
        {
            _currentUser = currentUser;
            _objectiveCampaignRepository = objectiveCampaignRepository;
            _promptProcessor = promptProcessor;
            _clientProfileRepository = clientProfileRepository;
            _textGenerationService = textGenerationService;
        }

        public async Task<Result<CampaignStrategy>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var clientId = _currentUser.UserId;

            if (clientId == null)
            {
                return Result<CampaignStrategy>.Failure("User are not authenticated");
            }

            var client = await _clientProfileRepository.GetById(clientId);

            if (client == null)
            {
                return Result<CampaignStrategy>.Failure("Informacoes da empresa nao preenchidas.");
            }

            var objectiveCampaign = await _objectiveCampaignRepository.GetById(request.ObjectiveCampaignId);

            if (objectiveCampaign == null)
            {
                return Result<CampaignStrategy>.Failure("Objetivo da campanha inválido.");
            }

            var campaing = new Campaign(
                clientProfileId: clientId,
                campaignName: request.CampaignName,
                externalCampaignName: "hive",
                objectiveCampaign: objectiveCampaign,
                objectiveCampaignId: request.ObjectiveCampaignId,
                budget: request.Budget,
                initial:request.InitialDate,
                end: request.EndDate
                );

            var (promptSystem, promptUser) = await _promptProcessor.PromptToGenerateTargeting(client, campaing);

            var targetingJson = await _textGenerationService.GenerateText(promptSystem, promptUser);

            if (targetingJson.IsFailure)
            {
                return Result<CampaignStrategy>.Failure("Erro ao gerar estrategia de campanha.");
            }

            var targeting = await _promptProcessor.DeserializeJson<CampaignStrategy>(targetingJson.Value!);

            return Result<CampaignStrategy>.Success(targeting);

        }
    }
}
