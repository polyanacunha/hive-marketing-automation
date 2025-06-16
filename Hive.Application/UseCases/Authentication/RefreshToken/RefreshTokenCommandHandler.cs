using Hive.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand>
    {
        private IAuthenticate _authenticate;

        public RefreshTokenCommandHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            await _authenticate.RefreshToken(request.ResfreshToken);
        }
    }
}
