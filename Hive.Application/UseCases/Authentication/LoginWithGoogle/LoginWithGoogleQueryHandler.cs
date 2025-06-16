using Hive.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.LoginWithGoogle
{
    public class LoginWithGoogleQueryHandler : IRequestHandler<LoginWithGoogleQuery>
    {
        private readonly IAuthenticate _authenticate;

        public LoginWithGoogleQueryHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task Handle(LoginWithGoogleQuery request, CancellationToken cancellation)
        {
            await _authenticate.LoginWithGoogle(request.email);
        }
    }
}
