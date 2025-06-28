using Hive.Application.DTOs;
using MediatR;


namespace Hive.Application.UseCases.Authentication.Login
{
    public record LoginUserQuery(string Email, string Password) : IRequest{ }
}
