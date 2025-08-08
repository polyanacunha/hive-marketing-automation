using FluentValidation;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Hive.Application.UseCases.Midia.UploadImage
{
    public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
    {
        private const long MaxFileSizeInBytes = 3 * 1024 * 1024;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".bmp" };
        private const int MaxImageWidth = 1920;
        private const int MaxImageHeight = 1080;
        private const int MaxFileCount = 10;

        public UploadImageCommandValidator()
        {
            RuleFor(x => x.AlbumName)
                .NotEmpty().WithMessage("Album name is required.");

            RuleFor(x => x.Files)
                .NotEmpty().WithMessage("No files were inserted.");

            RuleFor(x => x.Files.Count)
            .LessThanOrEqualTo(MaxFileCount)
            .WithMessage($"Não é possível enviar mais de {MaxFileCount} arquivos por vez.");

            // Usa .ForEach para aplicar um conjunto de regras a CADA arquivo na lista.
            RuleForEach(x => x.Files).ChildRules(file =>
            {
                // Validação de Tamanho (File Size)
                file.RuleFor(f => f.Length)
                    .NotNull()
                    .LessThanOrEqualTo(MaxFileSizeInBytes)
                    .WithMessage($"The file exceeds the maximum size of {MaxFileSizeInBytes / 1024 / 1024} MB.");

                // Validação de Extensão (Formato)
                file.RuleFor(f => f.FileName)
                    .Must(HaveAValidExtension)
                    .WithMessage($"Invalid file format. Allowed: {string.Join(", ", AllowedExtensions)}");

                // Validação de Resolução (Dimensões)
                file.RuleFor(f => f) // Passa o objeto IFormFile inteiro para o método de validação
                    .MustAsync(HaveValidDimensions)
                    .WithMessage($"The image exceeds the maximum dimensions of {MaxImageWidth}x{MaxImageHeight} pixels.");
            });
        }

        private bool HaveAValidExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && AllowedExtensions.Contains(extension);
        }

        private async Task<bool> HaveValidDimensions(IFormFile file, CancellationToken token)
        {
            if (file is null) return false;

            // Abre a stream para ler os metadados da imagem
            await using var stream = file.OpenReadStream();

            try
            {
                // O IdentifyAsync do ImageSharp é otimizado para ler apenas o cabeçalho do arquivo,
                // sem carregar a imagem inteira na memória. É muito eficiente.
                var imageInfo = await Image.IdentifyAsync(stream, token);
                return imageInfo.Width <= MaxImageWidth && imageInfo.Height <= MaxImageHeight;
            }
            catch
            {
                // Se o ImageSharp não consegue identificar, o arquivo está corrompido ou não é uma imagem válida.
                return false;
            }
        }
    }
}
