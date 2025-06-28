using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : IRequest
    {
    }
}
