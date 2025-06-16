using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Command
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IAuthenticate _authenticate;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(IAuthenticate authenticate, IEmailService emailService)
        {
            _authenticate = authenticate;
            _emailService = emailService;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _authenticate.UserExists(request.Email);

            if (userExists)
            {
                DomainExceptionValidation.When(true, "Email already registed");
            }

            var (userId, token) = await _authenticate.Register(request.Email, request.Password);
            var toEmail = request.Email;
            var subject = "Confirmacão de Email";
            var body = $"Clique no para confirmar email: https://localhost:7121?token={token}";

            await _emailService.SendEmail(toEmail, subject, body);

            return userId;
        }
    }
}
