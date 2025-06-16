using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Hive.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Crmf;
using System.Security.Claims;


namespace Hive.Infra.Data.Services
{
    public class Authenticate : IAuthenticate
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public Authenticate(IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _context = context;
        }

        public async Task ConfirmEmail(string userID, string token)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null) 
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                var erros = result.Errors.Select(e => e.Description);
                throw new Exception(string.Join(",", erros));
            }

        }

        public async Task<string?> GeneratePasswordResetToken(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null) {
                return null;
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> IsValidPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var (jwtToken, expirationDateInUtc) = _jwtTokenGenerator.GenerateJwtToken(new InfoUser(user.Id, user.UserName));

            var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshTokenExpirationdDateInUtc = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshTokenValue;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationdDateInUtc;

            await _userManager.UpdateAsync(user);

            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationdDateInUtc);
        }

        public async Task LoginWithGoogle(string email)
        {
            if (email == null)
            {
                throw new Exception("Google: Email is null");
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                {
                    var errorDescriptions = result.Errors.Select(e => e.Description);
                    var fullErrorMessage = string.Join("; ", errorDescriptions);
                    throw new Exception($"Unable to create the user:{fullErrorMessage}");
                }

                user = newUser;
            }

            var info = new UserLoginInfo("Google", email ?? string.Empty, "Google");

            var loginResult = await _userManager.AddLoginAsync(user, info);

            if (!loginResult.Succeeded)
            {
                var errorDescriptions = loginResult.Errors.Select(e => e.Description);
                var fullErrorMessage = string.Join("; ", errorDescriptions);
                throw new Exception($"Unable to login the user:{fullErrorMessage}");

            }

            var (jwtToken, expirationDateInUtc) = _jwtTokenGenerator.GenerateJwtToken(new InfoUser(user.Id, user.Email));

            var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshTokenExpirationdDateInUtc = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshTokenValue;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationdDateInUtc;

            await _userManager.UpdateAsync(user);

            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationdDateInUtc);
        }

        public async Task RefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("RefreshToken is missing.");
            }

            var user = await _context.Users
                .Include(u => u.RefreshToken)
                .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
            {
                throw new Exception("Enable to retrieve user for refresh token");
            }

            if (user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
            {
                throw new Exception("Refresh token is expired.");
            }

            var (jwtToken, expirationDateInUtc) = _jwtTokenGenerator.GenerateJwtToken(user);

            var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshTokenExpirationdDateInUtc = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshTokenValue;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationdDateInUtc;

            await _userManager.UpdateAsync(user);

            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationdDateInUtc);
        }

        public async Task<(string userId, string token)> Register(string email, string password)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errorDescriptions = result.Errors.Select(e => e.Description);
                var fullErrorMessage = string.Join("; ", errorDescriptions);
                throw new Exception(fullErrorMessage);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return (user.Id, token);
        }

        public async Task ResetPassword(string userID, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                var erros = result.Errors.Select(e => e.Description);
                throw new Exception(string.Join(",", erros));
            }

        }

        public async Task<bool> UserExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
