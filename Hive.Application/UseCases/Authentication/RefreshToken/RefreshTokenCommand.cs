using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.RefreshToken
{
    public record RefreshTokenCommand(string ResfreshToken) : IRequest<Result<Unit>>
    {
    }
}
