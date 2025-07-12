using Hive.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Amazon.S3;
using Hive.Infra.Data.Options;
using Microsoft.Extensions.Options;
using Amazon.S3.Model;


namespace Hive.Infra.Data.Services
{
    public class AwsBucketS3 : IStorageService
    {
        private readonly ILogger<AwsBucketS3> _logger;
        private readonly IAmazonS3 _amazonS3;
        private readonly AwsS3Settings _settings;

        public AwsBucketS3(ILogger<AwsBucketS3> logger, IAmazonS3 amazonS3, IOptions<AwsS3Settings> settings)
        {
            _logger = logger;
            _amazonS3 = amazonS3;
            _settings = settings.Value;
        }

        public async Task<string> GetFileFileAsync(string key, int timeInMinutes)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(timeInMinutes)
            };

            return _amazonS3.GetPreSignedURL(request);
        }

        public async Task<string> SaveFileAsync(Stream stream, string fileName)
        {
            try
            {
                var uniqueFileName = $"{Guid.NewGuid()}-{fileName}";

                var putRequest = new PutObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = uniqueFileName,
                    InputStream = stream,
                };

                await _amazonS3.PutObjectAsync(putRequest);

                _logger.LogInformation("Arquivo {FileName} salvo com sucesso no S3.", fileName);

                return uniqueFileName;
            }
            catch (AmazonS3Exception e)
            {
                _logger.LogError(e, "Erro do S3 ao fazer upload do arquivo {FileName}", fileName);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro inesperado ao fazer upload do arquivo {FileName} para o S3", fileName);
                throw;
            }
        }
    }
}
