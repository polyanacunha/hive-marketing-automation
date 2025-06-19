using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<Unit>>
    {
        private IAuthenticate _authenticate;

        public RefreshTokenCommandHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task<Result<Unit>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authenticate.RefreshToken(request.ResfreshToken);
        }
    }
}
