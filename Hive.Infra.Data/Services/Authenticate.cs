using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Validation;
using Hive.Infra.Data.Context;
using Hive.Infra.Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


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

        public async Task<Result<Unit>> ConfirmEmail(string userID, string token)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null)
            {
                return Result<Unit>.Failure("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                var erros = result.Errors.Select(e => e.Description);
                return Result<Unit>.Failure(erros);

            }
            return Result<Unit>.Success(Unit.Value);
        }
        
        public async Task<Result<string>> GeneratePasswordResetToken(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null) {
                return Result<string>.Failure("User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (token == null)
            {
                return Result<string>.Failure("An error occurred while generating token");
            }

            return Result<string>.Success(token);
        }

        public async Task<Result<bool>> IsValidPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Result<bool>.Failure("User not found");
            }

            var result = await _userManager.CheckPasswordAsync(user, password);

            return Result<bool>.Success(result);
        }

        public async Task<Result<Unit>> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var (jwtToken, expirationDateInUtc) = _jwtTokenGenerator.GenerateJwtToken(new InfoUser(user.Id.ToString(), user.UserName));

            var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshTokenExpirationdDateInUtc = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshTokenValue;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationdDateInUtc;

            await _userManager.UpdateAsync(user);

            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationdDateInUtc);

            return Result<Unit>.Success(Unit.Value);
        }

        public Task<Result<Unit>> LoginWithFacebook()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Unit>> LoginWithGoogle(string email)
        {
            if (email == null)
            {
                return Result<Unit>.Failure("Email is null");
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
                    return Result<Unit>.Failure(errorDescriptions);

                }

                user = newUser;
            }

            var info = new UserLoginInfo("Google", email ?? string.Empty, "Google");

            var loginResult = await _userManager.AddLoginAsync(user, info);

            if (!loginResult.Succeeded)
            {
                var errorDescriptions = loginResult.Errors.Select(e => e.Description);
                return Result<Unit>.Failure(errorDescriptions);
            }

            var (jwtToken, expirationDateInUtc) = _jwtTokenGenerator.GenerateJwtToken(new InfoUser(user.Id.ToString(), user.Email!));

            var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshTokenExpirationdDateInUtc = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshTokenValue;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationdDateInUtc;

            await _userManager.UpdateAsync(user);

            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationdDateInUtc);

            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<Unit>> RefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Result<Unit>.Failure("RefreshToken is missing.");
            }

            var user = await _context.Users
                .Include(u => u.RefreshToken)
                .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
            {
                return Result<Unit>.Failure("Refresh token invalid.");
            }

            if (user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
            {
                return Result<Unit>.Failure("Refresh token is expired.");
            }

            var (jwtToken, expirationDateInUtc) = _jwtTokenGenerator.GenerateJwtToken(new InfoUser(user.Id.ToString(), user.UserName!));

            var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshTokenExpirationdDateInUtc = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshTokenValue;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationdDateInUtc;

            await _userManager.UpdateAsync(user);

            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _jwtTokenGenerator.WriteAuthTokenAsHttpOnlyCookie("HIVE_REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationdDateInUtc);

            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<(string userId, string token)>> Register(string email, string password)
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
                return Result< (string userId, string token)>.Failure(errorDescriptions);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return Result<(string userId, string token)>.Success((user.Id.ToString(), token));
        }

        public async Task<Result<Unit>> ResetPassword(string userID, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null)
            {
                return Result<Unit>.Failure("User not found");
            }
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<Unit>.Failure(errors);
            }

            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<bool>> UserExists(string email)
        {
            var result = await _userManager.FindByEmailAsync(email) != null;
            return Result<bool>.Success(result);
        }
    }
}
