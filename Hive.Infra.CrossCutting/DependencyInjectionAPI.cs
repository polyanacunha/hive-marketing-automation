using Hangfire;
using Hangfire.PostgreSql;
using Hive.Application.Interfaces;
using Hive.Application.Mappings;
using Hive.Domain.Entities;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Hive.Infra.Data.Identity;
using Hive.Infra.Data.Repositories;
using Hive.Infra.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Hive.Infra.CrossCutting;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(option => {
            option.Password.RequiredLength = 8;
            option.Password.RequireUppercase = true;
            option.Password.RequireLowercase = true;
            option.Password.RequireDigit = true;
            option.Password.RequireNonAlphanumeric = true;
            option.User.RequireUniqueEmail = true;

        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options =>{
            options.TokenLifespan = TimeSpan.FromHours(2);
        });

        services.AddHangfire(config => config
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(options =>
        {
            options.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }));

        services.AddHangfireServer();


        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"
        ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

      
        services.AddScoped<ICampaingRepository, CampaingRepository>();
        services.AddScoped<IMidiaRepository, MidiaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMarketSegmentRepository, MarketSegmentRepository>();
        services.AddScoped<ITargetAudienceRepository, TargetAudienceRepository>();
        services.AddScoped<IClientProfileRepository, ClientProfileRepository>();

        services.AddScoped<IAuthenticate, Authenticate>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddTransient<IEmailService, EmailService>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        services.AddHttpContextAccessor();

        var myhandlers = AppDomain.CurrentDomain.Load("Hive.Application");
        //services.AddMediatR(myhandlers);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));

        return services;
    }   
}
