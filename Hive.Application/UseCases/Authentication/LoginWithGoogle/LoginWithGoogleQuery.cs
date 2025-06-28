using Hive.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.UseCases.Authentication.LoginWithGoogle
{
    public record LoginWithGoogleQuery(string Email) : IRequest<Result<Unit>>
    {
    }
}
