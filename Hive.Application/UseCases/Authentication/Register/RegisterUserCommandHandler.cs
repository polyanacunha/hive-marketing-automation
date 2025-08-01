using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Command
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly IAuthenticate _authenticate;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(IAuthenticate authenticate, IEmailService emailService)
        {
            _authenticate = authenticate;
            _emailService = emailService;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _authenticate.UserExists(request.Email);

            if (userExists.Value) 
            {
                return Result<string>.Failure("Email already registered");            
            }

            var result = await _authenticate.Register(request.Email, request.Password);

            if (result.IsFailure) 
            {
                return Result<string>.Failure(result.Errors);
            }

            var (userId, token) = result.Value;
            var toEmail = request.Email;
            var subject = "Confirmacão de Email";
            var body = $"Clique no para confirmar email: https://localhost:4200?token={token}&userId={userId}";

            await _emailService.SendEmail(toEmail, subject, body);

            return Result<string>.Success(userId);
        }
    }
}
