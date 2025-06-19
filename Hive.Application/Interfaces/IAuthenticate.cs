using Hive.Application.DTOs;
using Hive.Domain.Validation;
using MediatR;
using System.Numerics;

namespace Hive.Application.Interfaces
{
    public interface IAuthenticate
    {
        Task<Result<(string userId, string token)>> Register(string email, string password);
        Task<Result<Unit>> Login(string email, string password);
        Task<Result<Unit>> RefreshToken(string? refreshToken);
        Task<Result<Unit>> LoginWithGoogle(string email);
        Task<Result<bool>> UserExists(string email);
        Task<Result<bool>> IsValidPassword(string email, string password);
        Task<Result<string>> GeneratePasswordResetToken(string email);
        Task<Result<Unit>> ConfirmEmail(string userID, string token);
        Task<Result<Unit>> ResetPassword(string userID, string token, string newPassword);
    }
}
    