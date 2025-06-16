using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.RecoverPassword
{
    public record ResetPasswordCommand(string UserId, string Token, string Password) : IRequest
    {
    }
}
