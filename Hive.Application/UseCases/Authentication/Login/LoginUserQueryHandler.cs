using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery>
    {
        private readonly IAuthenticate _authenticate;

        public LoginUserQueryHandler(IAuthenticate autenticate)
        {
            _authenticate = autenticate;
        }

        public async Task Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var isValidPassword = await _authenticate.IsValidPassword(request.Email, request.Password);

            if(!isValidPassword)
            {
                DomainExceptionValidation.When(true, "Email or password invalid.");
            }

            await _authenticate.Login(request.Email, request.Password);

        }
    }
}
