using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.LoginWithGoogle
{
    public class LoginWithGoogleQueryHandler : IRequestHandler<LoginWithGoogleQuery, Result<Unit>>
    {
        private readonly IAuthenticate _authenticate;

        public LoginWithGoogleQueryHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task<Result<Unit>> Handle(LoginWithGoogleQuery request, CancellationToken cancellationToken)
        {
            return await _authenticate.LoginWithGoogle(request.Email);
        }
    }
}
