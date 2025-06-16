using Hive.Application.DTOs;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Command
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<string>;
}
