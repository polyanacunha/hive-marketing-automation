using Hive.Application.Interfaces;
using Hive.Infra.Data.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Net;
using Hive.Application.DTOs;
using Hive.Domain.Validation;
using System.Net.Http.Json;
using Hive.Infra.Data.NovaPasta;


namespace Hive.Infra.Data.Services
{
    public class ChatGptTextGenerator : ITextGenerationService
    {
        private readonly OpenAiSettings _openAiSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ChatGptTextGenerator> _logger;
        private const string URL_BASE = "https://api.openai.com/v1/chat/completions";

        public ChatGptTextGenerator(IOptions<OpenAiSettings> openAiSettings, IHttpClientFactory httpClientFactory, ILogger<ChatGptTextGenerator> logger)
        {
            var d = openAiSettings.Value?.ApiKey;
            if (string.IsNullOrEmpty(openAiSettings.Value?.ApiKey))
            {
                throw new ArgumentNullException(nameof(openAiSettings), "API Key da OpenAI não foi configurada.");
            }
            _openAiSettings = openAiSettings.Value;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<Result<string>> GenerateText(string promptSystem, string promptUser)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAiSettings.ApiKey);

                var requestBody = new
                {
                    model = _openAiSettings.ChatGptModel, 
                    messages = new[]
                    {
                        new { role = "system", content = promptSystem },
                        new { role = "user", content = promptUser }
                    }
                };

                var jsonRequest = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(URL_BASE, content);

                if(!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Falha na API da OpenAI. Status: {StatusCode}. Resposta: {Response}",
                        response.StatusCode,
                        errorContent);

                    return Result<string>.Failure(errorContent);

                }

                var openAiResponse = await response.Content.ReadFromJsonAsync<OpenAiChatResponse>();

                var scriptJsonString = openAiResponse?.Choices?.FirstOrDefault()?.Message?.Content
                    ?? throw new Exception($"Erro to text generator");

                return Result<string>.Success(scriptJsonString);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao solicitar requisicao ao ChatGpt.");
                throw new Exception($"Erro to text generator:{ex.Message}");
            }
        }
    }
}


//return Task.FromResult(
//    "{\"script\":[" +
//    "{\"scene_number\":1,\"duration_seconds\":5,\"visual_prompt\":\"Close-up shot of a sleek, modern running shoe floating mid-air against a clean white background, soft shadows, cinematic lighting, product showcase style, hyperrealistic\",\"on_screen_text\":\"THE NEW LAUNCH\",\"narrator_script\":\"Introducing the latest from Pés Velozes...\"}," +
//    "{\"scene_number\":2,\"duration_seconds\":6,\"visual_prompt\":\"Medium shot of a young runner (around 30 years old, athletic build) tying their running shoes on a rooftop at sunrise, golden hour lighting, city skyline in the background, minimalistic outfit, cinematic lens flare\",\"on_screen_text\":\"Lightweight. Secure. Ready.\",\"narrator_script\":\"Lightweight as air, built to go the distance.\"}," +
//    "{\"scene_number\":3,\"duration_seconds\":6,\"visual_prompt\":\"Wide shot of the runner in slow motion sprinting across a bridge with motion blur on background, sunrise tones, mist in the air, hyperrealistic details, smooth camera tracking\",\"on_screen_text\":\"Feel the Comfort\",\"narrator_script\":\"Every step feels effortless. Every run feels right.\"}," +
//    "{\"scene_number\":4,\"duration_seconds\":6,\"visual_prompt\":\"Montage: Diverse runners (men and women, 25-45) running in various settings – park, urban trail, treadmill at gym – all wearing the same new running shoe, stylized with seamless transitions, vibrant color grading\",\"on_screen_text\":\"Engineered for You\",\"narrator_script\":\"Designed for passion. Made for performance.\"}," +
//    "{\"scene_number\":5,\"duration_seconds\":7,\"visual_prompt\":\"Close-up spinning shot of the running shoe on a dark background with dynamic lighting highlights and floating specs of dust, product focus style, modern and minimal, slow rotation\",\"on_screen_text\":\"CLICK THE LINK IN BIO\",\"narrator_script\":\"Get yours today. Click the link in the bio!\"}" +
//    "]}"
//);