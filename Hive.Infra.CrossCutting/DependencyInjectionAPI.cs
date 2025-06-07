using Hive.Application.Interfaces;
using Hive.Application.Mappings;
using Hive.Application.Services;
using Hive.Domain.Account;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Hive.Infra.Data.Identity;
using Hive.Infra.Data.Repositories;
using MediatR;
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
        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
        ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ICampaingRepository, CampaingRepository>();
        services.AddScoped<IMidiaRepository, MidiaRepository>();
        services.AddScoped<ICampaingService, CampaingService>();
        services.AddScoped<IMidiaService, MidiaService>();

        services.AddScoped<IAuthenticate, AuthenticateService>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        var myhandlers = AppDomain.CurrentDomain.Load("Hive.Application");
        //services.AddMediatR(myhandlers);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));

        return services;
    }
}
