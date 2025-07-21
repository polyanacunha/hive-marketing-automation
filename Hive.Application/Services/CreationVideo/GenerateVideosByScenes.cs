using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Domain.Enum;
using Hive.Domain.Interfaces;


namespace Hive.Application.Services.CreationVideo
{
    public class GenerateVideosByScenes : IGenerateVideosByScenes
    {
        private readonly IMidiaProductionRepository _midiaProductionRespository;
        private readonly IPromptProcessor _promptProcessor;
        private readonly ITextGenerationService _textGenerationService;
        private readonly IJobGenerationRepository _jobGenerationRepository;
        private readonly IVideoGenerator _videoGenerator;
        private readonly IBackgroundScheduler _backgroundScheduler;
        private readonly IUnitOfWork _unitOfWork;

        public GenerateVideosByScenes(IMidiaProductionRepository midiaProductionRespository, ITextGenerationService textGenerationService, IJobGenerationRepository jobGenerationRepository, IVideoGenerator videoGenerator, IUnitOfWork unitOfWork, IBackgroundScheduler backgroundScheduler, IPromptProcessor promptProcessor)
        {
            _midiaProductionRespository = midiaProductionRespository;
            _textGenerationService = textGenerationService;
            _jobGenerationRepository = jobGenerationRepository;
            _videoGenerator = videoGenerator;
            _unitOfWork = unitOfWork;
            _backgroundScheduler = backgroundScheduler;
            _promptProcessor = promptProcessor;
        }

        public async Task GenerateScript(int videoProductionId, CancellationToken cancellationToken)
        {
            var production = await _midiaProductionRespository.GetById(videoProductionId);

            if (production == null)
            {
                return;
            }

            // 1. Gera o roteiro
            var scriptJson = await _textGenerationService.GenerateText(production.SystemPrompt, production.UserPrompt);

            if(scriptJson.IsFailure)
            {
                production.MarkToFailed();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return;
            }

            var script = await _promptProcessor.DeserializeJson<ScriptDto>(scriptJson.Value!);

            // 2. Atualiza o estado da Produção
            production.AddGeneratedScriptJson(scriptJson.Value!);
            production.MarkToScriptGenerated();

            await _backgroundScheduler.ScheduleStitchingJob(videoProductionId);

            // 4. Cria e agenda um job para cada cena (Fan-Out)
            var jobGroupName = $"scenes-for-production-{videoProductionId}";

            foreach (var scene in script.Script)
            {
                var id = Guid.NewGuid();
                // Salva o registro da cena no nosso banco
                var job = new JobGeneration(
                    id: id,
                    midiaProductionId: videoProductionId,
                    midiaProduction: production,
                    prompt: scene.VisualPrompt,
                    assetType: AssetType.VIDEO
                    );

                production.JobsGenerations.Add(job);

                await _backgroundScheduler.ScheduleSceneProcessingJob(id, production.Id, jobGroupName);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task MarkSceneAsFailedPermanently(Guid videoProductionId, CancellationToken cancellationToken)
        {
            var jobEntity = await _jobGenerationRepository.GetById(videoProductionId);

            if (jobEntity is not null)
            {
                jobEntity.MarkToFailed();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            
        }

        public async Task ProcessSingleScene(Guid sceneJobId, int mediaId, CancellationToken cancellationToken)
        {
            var job = await _jobGenerationRepository.GetById(sceneJobId);

            if (job is null)
            {
                return;
            }

            job.MarkToProcessing();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var urlsList = job.MidiaProduction.InputImageUrl
                .Select(imageUrl => imageUrl.ImageKey)
                .ToList();

            var urlScene = await _videoGenerator.Generator(job.Prompt, urlsList);

            job.SetExternalMediaUrl(urlScene);
            job.MarkToCompleted();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task StitchFinalVideo(int videoProductionId, CancellationToken cancellationToken)
        {
            var finalUrl = "https://urlfinal.com";

            var production = await _midiaProductionRespository.GetById(videoProductionId)
                ?? throw new Exception("Media not found");

            production.SetFinalVideourl(finalUrl);
            production.MarkToCompleted();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
