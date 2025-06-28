using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.ConfirmEmail
{
    public record ConfirmEmailCommand(string UserId, string Token) : IRequest
    {
    }
}
