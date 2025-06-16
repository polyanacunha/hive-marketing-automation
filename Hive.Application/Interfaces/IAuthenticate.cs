using Hive.Application.DTOs;

namespace Hive.Application.Interfaces
{
    public interface IAuthenticate
    {
        Task<(string userId, string token)> Register(string email, string password);
        Task Login(string email, string password);
        Task RefreshToken(string? refreshToken);
        Task LoginWithGoogle(string email);
        Task<bool> UserExists(string email);
        Task<bool> IsValidPassword(string email, string password);
        Task<string?> GeneratePasswordResetToken(string email);
        Task ConfirmEmail(string userID, string token);
        Task ResetPassword(string userID, string token, string newPassword);
    }
}
    