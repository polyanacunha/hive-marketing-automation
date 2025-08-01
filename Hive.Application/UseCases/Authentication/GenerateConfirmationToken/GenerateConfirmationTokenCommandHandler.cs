using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.GenerateConfirmationToken
{
    public class GenerateConfirmationTokenCommandHandler : IRequestHandler<GenerateConfirmationTokenCommand, Result<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IAuthenticate _authenticate;

        public GenerateConfirmationTokenCommandHandler(IEmailService emailService, IAuthenticate authenticate)
        {
            _emailService = emailService;
            _authenticate = authenticate;
        }

        public async Task<Result<string>> Handle(GenerateConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticate.GenerateEmailConfirmationToken(request.Email);

            if (result.IsFailure)
            {
                return Result<string>.Failure(result.Errors);
            }

            var (userId, token) = result.Value;
            var toEmail = request.Email;
            var subject = "Confirmacão de Email";
            var body = $"Clique no para confirmar email: https://localhost:7121?token={token}&userId={userId}";

            await _emailService.SendEmail(toEmail, subject, body);

            return Result<string>.Success("Um link de verificação foi enviado ao seu email.");
        }
    }
}
