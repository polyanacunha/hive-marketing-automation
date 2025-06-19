using Hive.Application.DTOs;
using Hive.Domain.Validation;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Command
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Result<string>>;
}
