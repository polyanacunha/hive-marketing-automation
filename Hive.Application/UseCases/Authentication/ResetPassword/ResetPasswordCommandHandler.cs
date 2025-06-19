using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Authentication.RecoverPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<Unit>>
    {
        private readonly IAuthenticate _authenticate;

        public ResetPasswordCommandHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task<Result<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authenticate.ResetPassword(request.UserId, request.Token, request.Password);
        }
    }
}
