using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<Unit>>
    {
        private readonly IAuthenticate _authenticate;

        public LoginUserQueryHandler(IAuthenticate autenticate)
        {
            _authenticate = autenticate;
        }

        public async Task<Result<Unit>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return await _authenticate.Login(request.Email, request.Password);
        }
    }
}
