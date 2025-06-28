using Hive.Application.Interfaces;
using MediatR;

namespace Hive.Application.UseCases.Authentication.RecoverPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IAuthenticate _authenticate;

        public ResetPasswordCommandHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _authenticate.ResetPassword(request.UserId,request.Token, request.Password);
        }
    }
}
