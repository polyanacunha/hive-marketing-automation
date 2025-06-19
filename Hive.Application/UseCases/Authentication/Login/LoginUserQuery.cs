using Hive.Application.DTOs;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Login
{
    public record LoginUserQuery(string Email, string Password) : IRequest<Result<Unit>>
    {}
}
