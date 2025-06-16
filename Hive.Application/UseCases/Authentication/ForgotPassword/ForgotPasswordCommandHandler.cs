using Hive.Application.Interfaces;
using MediatR;

namespace Hive.Application.UseCases.Authentication.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IEmailService _emailService;
        private readonly IAuthenticate _authenticate;

        public ForgotPasswordCommandHandler(IEmailService emailService, IAuthenticate authenticate)
        {
            _emailService = emailService;
            _authenticate = authenticate;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var token = await _authenticate.GeneratePasswordResetToken(request.Email);

            if (token is null)
            {
                return;
            }

            var toEmail = request.Email;
            var subject = "Recuperação de Senha";
            var body = $"Seu link de recuperação é: https://localhost:7121?token={token}";

            await _emailService.SendEmail(toEmail, subject, body);
        }
    }
}
