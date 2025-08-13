using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Authentication.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IAuthenticate _authenticate;

        public ForgotPasswordCommandHandler(IEmailService emailService, IAuthenticate authenticate)
        {
            _emailService = emailService;
            _authenticate = authenticate;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var token = await _authenticate.GeneratePasswordResetToken(request.Email);

            if(token.IsFailure)
            {
                return Result<string>.Failure(token.Errors);
            }

            var toEmail = request.Email;
            var subject = "Recuperação de Senha";
            var body = $"Seu link de recuperação é: https://localhost:4200?token={token.Value}";

            await _emailService.SendEmail(toEmail, subject, body);

            return Result<string>.Success("Enviamos um e-mail com o link para redefinir sua senha. Verifique sua caixa de entrada (e o spam também, se necessário).");
        }
    }
}
