using Hive.Application.DTOs;
using Hive.Application.DTOs.Meta;
using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using Hive.Infra.Data.Dtos.Meta;
using Hive.Infra.Data.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Hive.Infra.Data.Services
{
    public class MetaApi : IMetaApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MetaApiSettings _metaApiSettings;
        private readonly ILogger<EmailService> _logger;

        public MetaApi(IHttpClientFactory httpClientFactory, IOptions<MetaApiSettings> metaApiSettings, ILogger<EmailService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _metaApiSettings = metaApiSettings.Value;
            _logger = logger;
        }

        public Result<string> GetUrlRedirect()
        {
            var authUrl = $"{_metaApiSettings.FacebookUrlBase}dialog/oauth" +
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

            var tokenUrl = $"{_metaApiSettings.MarketingApiUrlBase}oauth/access_token" +
                           $"?client_id={_metaApiSettings.ClientId}" +
                           $"&redirect_uri={Uri.EscapeDataString(_metaApiSettings.RedirectUri)}" +
                           $"&client_secret={_metaApiSettings.ClientSecret}" +
                           $"&code={Code}";

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(tokenUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var metaError = JsonSerializer.Deserialize<MetaError>(errorContent);
                var MessageError = VerifyMetaError(response.StatusCode, metaError);

                return Result<string>.Failure(MessageError);
            }

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(json))
            {
                return Result<string>.Failure("erro ao ler json");
            }
            
            return Result<string>.Success(json);
        }

        public async Task<Result<string>> GetInfoUser(string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return Result<string>.Failure("Token de autorização ausente.");

            var secretProof = GenerateAppSecretProof(AccessToken);

            var request = $"{_metaApiSettings.MarketingApiUrlBase}" + 
                          "me/adaccounts?fields=id,name,account_status" + 
                          $"&appsecret_proof={secretProof}";

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var metaError = JsonSerializer.Deserialize<MetaError>(errorContent);
                var MessageError = VerifyMetaError(response.StatusCode, metaError);

                return Result<string>.Failure(MessageError);
            }

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(json))
            {
                return Result<string>.Failure("erro ao ler json");
            }

            return Result<string>.Success(json);
        }

        public async Task<Result<string>> GetAllPages(string AdAccountId, string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return Result<string>.Failure("Token de autorização ausente.");

            var secretProof = GenerateAppSecretProof(AccessToken);

            var request = $"{_metaApiSettings.MarketingApiUrlBase}" +
                          $"/act_{AdAccountId}/promotable_pages" +
                          "?fields=id,name,picture{url}" +
                          $"?appsecret_proof={secretProof}";

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var metaError = JsonSerializer.Deserialize<MetaError>(errorContent);
                var MessageError = VerifyMetaError(response.StatusCode, metaError);

                return Result<string>.Failure(MessageError);
            }

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(json))
            {
                return Result<string>.Failure("erro ao ler json");
            }

            return Result<string>.Success(json);
        }

        public async Task<Result<List<Interest>>> SearchInterests(List<string> Interest, string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return Result<List<Interest>>.Failure("Token de autorização ausente.");

            var secretProof = GenerateAppSecretProof(AccessToken);

            var errors = new List<string>();

            var  idInterests= new List<Interest>();

            foreach (var interest in Interest)
            {
                try
                {
                    var request = $"{_metaApiSettings.MarketingApiUrlBase}" +
                          "/search" +
                          "?type=adinterest" +
                          $"&q={Uri.EscapeDataString(interest)}" +
                          $"&sort=audience_size" +
                          $"&appsecret_proof={secretProof}";

                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                    var response = await client.GetAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        var metaError = JsonSerializer.Deserialize<MetaError>(errorContent);
                        var MessageError = VerifyMetaError(response.StatusCode, metaError);

                        errors.Add($"Não foi encontrado um grupo de interesse compatível com {interest}.");
                        continue;
                        
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<ResponseDataMeta<Interest>>(json);

                    idInterests.Add(result!.Data.First());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Api Meta: {ex.Message}");
                }
            }

            return Result<List<Interest>>.Success(idInterests);
        }

        public async Task<Result<List<MetaInterestConfig>>> SearchInterestsBatch(List<string> interests, string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                return Result<List<MetaInterestConfig>>.Failure("Token de autorização ausente.");

            var secretProof = GenerateAppSecretProof(accessToken);
            var client = _httpClientFactory.CreateClient();

            var batchItems = interests.Select(interest => new
            {
                method = "GET",
                relative_url = $"search?type=adinterest&q={Uri.EscapeDataString(interest)}&sort=audience_size&appsecret_proof={secretProof}"
            }).ToList();

            var batchRequest = new
            {
                access_token = accessToken,
                batch = batchItems
            };

            var content = new StringContent(JsonSerializer.Serialize(batchRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_metaApiSettings.MarketingApiUrlBase, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar batch: {0}", errorContent);
                return Result<List<MetaInterestConfig>>.Failure($"Falha ao realizar requisição em batch. Status: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var responseArray = JsonSerializer.Deserialize<List<BatchResponse>>(json);

            var results = new List<MetaInterestConfig>();

            foreach (var item in responseArray!)
            {
                if (item.code == 200)
                {
                    try
                    {
                        var dataResult = JsonSerializer.Deserialize<ResponseDataMeta<InterestResponse>>(item.body);

                        if (dataResult?.Data?.Any() == true)
                            results.Add(new MetaInterestConfig()
                            {
                                Id = dataResult.Data.First().Id
                            });
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogWarning(ex, "Falha ao desserializar corpo de um item do batch: {Body}", item.body);
                    }
                }
                else
                {
                    _logger.LogWarning("Erro em item do batch (code {Code}): {Body}", item.code, item.body);
                }
            }

            // Retorna sucesso com a lista de resultados (pode estar vazia).
            return Result<List<MetaInterestConfig>>.Success(results);
        }
        
        public async Task<Result<string>> SearchRegion(string RegionName,string CountryCode, string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return Result<string>.Failure("Token de autorização ausente.");

            var secretProof = GenerateAppSecretProof(AccessToken);

            try
            {
                var request = $"{_metaApiSettings.MarketingApiUrlBase}" +
                        "/search" +
                        "?type=adgeolocation" +
                        $"&country_code={CountryCode}" +
                        $"&q={Uri.EscapeDataString(RegionName)}" +
                        $"&appsecret_proof={secretProof}";

                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var response = await client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var metaError = JsonSerializer.Deserialize<MetaError>(errorContent);
                    var MessageError = VerifyMetaError(response.StatusCode, metaError);

                    return Result<string>.Failure($"Não foi encontrado uma referência para a localidade {RegionName}.");
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ResponseDataMeta<SearchRegion>>(json)!;

                var region = result.Data
                    .Where(d => d.Type == "region" && d.CountryCode == CountryCode)
                    .FirstOrDefault()!;

                return Result<string>.Success(region.Key);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Api Meta: {ex.Message}");

                return Result<string>.Failure($"Ocorreu um erro na busca pela referência para a localidade {RegionName}.");
            }
        }
       
        private string VerifyMetaError(HttpStatusCode statusCode, MetaError? metaError) 
        {
            if (metaError is null)
            {
                return $"Erro desconhecido da API com status {statusCode}.";
            }

            switch (metaError.Code)
            {
                case 190: // Erro comum de token de acesso inválido ou expirado
                    return "Seu token de acesso para a Meta expirou ou é inválido. Por favor, reconecte sua conta.";
                case 100: // Parâmetro inválido
                    return $"Parâmetro inválido na requisição para a Meta: {metaError.Message}";
                case 803: // Problema de permissão
                    return "Sua conta não tem permissão para realizar esta ação na Meta.";

                // Este é um exemplo de erro de regra de negócio que você mencionou
                case var code when (code >= 2000 && code <= 2999): // Erros de processamento de anúncio
                    throw new Exception("Falha ao contatar api da Meta");

                default:
                    throw new Exception("Falha ao contatar api da Meta");
            }
        }

        private string GenerateAppSecretProof(string accessToken)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_metaApiSettings.ClientSecret)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(accessToken));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public async Task<Result<string>> CreateCampaign(MetaCampaignConfig content, string AccessToken, string AccountId)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return Result<string>.Failure("Token de autorização ausente.");

            var secretProof = GenerateAppSecretProof(AccessToken);

            try
            {
                var request = $"{_metaApiSettings.MarketingApiUrlBase}" +
                        $"/act_{AccountId}/campaings" +
                        $"&appsecret_proof={secretProof}";

                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var jsonPayload = JsonSerializer.Serialize(content);

                using var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(request, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var metaError = JsonSerializer.Deserialize<MetaError>(errorContent);
                    var MessageError = VerifyMetaError(response.StatusCode, metaError);

                    return Result<string>.Failure($"");
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<CreateCampaignMetaResponse>(json)!;

                return Result<string>.Success(result.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Api Meta: {ex.Message}");

                return Result<string>.Failure($"Ocorreu um erro na criacão de campanha na Meta.");
            }
        }

        public Task<Result<string>> CreateAdSet(MetaAdSetConfig content, string AccessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<AdAccount>>> GetAllAdAccounts(string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return Result<List<AdAccount>>.Failure("Token de autorização ausente.");

            var secretProof = GenerateAppSecretProof(AccessToken);

            var request = $"{_metaApiSettings.MarketingApiUrlBase}" +
                          "me/adaccounts?fields=id,name" +
                          $"&appsecret_proof={secretProof}";

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var metaError = JsonSerializer.Deserialize<MetaError>(errorContent);
                var MessageError = VerifyMetaError(response.StatusCode, metaError);

                return Result<List<AdAccount>>.Failure(MessageError);
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseDataMeta<AdAccount>>(json);

            if (string.IsNullOrEmpty(json))
            {
                return Result<List<AdAccount>>.Failure("erro ao ler json");
            }

            return Result<List<AdAccount>>.Success(result!.Data);
        }
    }
}
 