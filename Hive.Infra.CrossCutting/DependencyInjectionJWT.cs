using Hive.Infra.Data.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hive.Infra.CrossCutting;

public static class DependencyInjectionJWT
{
    public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.Configure<JwtOptions>( configuration.GetSection(JwtOptions.JwtOptionKey));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddCookie().AddGoogle(options =>
        {
            var ClientId = configuration["Authentication:Google:ClientId"];

            if (ClientId == null)
            {
                throw new ArgumentException(nameof(ClientId));
            }

            var ClientSecret = configuration["Authentication:Google:ClientSecret"];

            if (ClientSecret == null)
            {
                throw new ArgumentException(nameof(ClientSecret));
            }

            options.ClientId = ClientId;
            options.ClientSecret = ClientSecret;
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            var jwtOptions = configuration.GetSection(JwtOptions.JwtOptionKey).Get<JwtOptions>()
                ?? throw new ArgumentException(nameof(JwtOptions));

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["HIVE_ACCESS_TOKEN"];
                    return Task.CompletedTask;
                }
            };
        });


        services.AddAuthorization();
        return services;
    }
}
