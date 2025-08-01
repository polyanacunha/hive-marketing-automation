using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using MediatR;

namespace Hive.Application.UseCases.Authentication.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<Unit>>
    {
        private readonly IAuthenticate _authenticate;

        public ConfirmEmailCommandHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task<Result<Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _authenticate.ConfirmEmail(request.UserId, request.Token);
        }
    }
}
