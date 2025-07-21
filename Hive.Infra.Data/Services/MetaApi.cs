using Google.Cloud.AIPlatform.V1;
using Google.Rpc;
using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using Hive.Infra.Data.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace Hive.Infra.Data.Services
{
    public class MetaApi : IMetaApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MetaApiSettings _metaApiSettings;

        public MetaApi(IHttpClientFactory httpClientFactory, IOptions<MetaApiSettings> metaApiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _metaApiSettings = metaApiSettings.Value;
        }

        public async Task<Result<string>> GetUrlRedirect()
        {
            var authUrl = $"{_metaApiSettings.FacebookUrlBase}/dialog/oauth" +
                      $"?client_id={_metaApiSettings.ClientId}" +
                      $"&redirect_uri={Uri.EscapeDataString(_metaApiSettings.RedirectUri)}" +
                      $"&state={Guid.NewGuid()}" +
                      $"&response_type=code" +
                      $"&scope={Uri.EscapeDataString(_metaApiSettings.Scopes)}";

            return Result<string>.Success(authUrl);
        }

        public async Task<Result<string>> GetMetaAccessToken(string Code)
        {
            if (string.IsNullOrEmpty(Code))
                return Result<string>.Failure("Código de autorização ausente.");

            var tokenUrl = $"{_metaApiSettings.MarketingApiUrlBase}/oauth/access_token" +
                           $"?client_id={_metaApiSettings.ClientId}" +
                           $"&redirect_uri={Uri.EscapeDataString(_metaApiSettings.RedirectUri)}" +
                           $"&client_secret={_metaApiSettings.ClientSecret}" +
                           $"&code={Code}";

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(tokenUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return Result<string>.Failure($"Erro ao obter o token: {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(json))
            {
                return Result<string>.Failure("erro ao ler json");
            }
            
            return Result<string>.Success(json);
        }
    }
}
