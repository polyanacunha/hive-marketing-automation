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
            var isValid = await _authenticate.IsValidPassword(request.Email, request.Password);

            if (isValid.IsFailure) 
            {
                return Result<Unit>.Failure(isValid.Errors);
            }

            if (!isValid.Value)
            {
                return Result<Unit>.Failure("Email or password invalid.");
            }

            return await _authenticate.Login(request.Email, request.Password);
        }
    }
}
