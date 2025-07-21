using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using Hive.Application.Interfaces;
using Hive.Application.Mappings;
using Hive.Application.Services.CreationVideo;
using Hive.Application.Services.ProcessPrompt;
using Hive.Application.Shared;
using Hive.Domain.Interfaces;
using Hive.Infra.Data.Context;
using Hive.Infra.Data.Identity;
using Hive.Infra.Data.Options;
using Hive.Infra.Data.Repositories;
using Hive.Infra.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
namespace Hive.Infra.CrossCutting;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.Configure<SmtpSettings>(configuration.GetSection(SmtpSettings.SmtpSettingsKey));
        services.Configure<OpenAiSettings>(configuration.GetSection(OpenAiSettings.OpenAiSettingsKey));
        services.Configure<AwsS3Settings>(configuration.GetSection(AwsS3Settings.AwsS3SettingsKey));
        services.Configure<MetaApiSettings>(configuration.GetSection(MetaApiSettings.MetaApiSettingsKey));
        services.Configure<Encryption>(configuration.GetSection(Encryption.EncryptionKey));


        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"
        ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>(option =>
        {
            option.Password.RequiredLength = 8;
            option.Password.RequireUppercase = true;
            option.Password.RequireLowercase = true;
            option.Password.RequireDigit = true;
            option.Password.RequireNonAlphanumeric = true;
            option.User.RequireUniqueEmail = true;

        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        var awsSettings = configuration.GetSection("AwsS3Settings");

        // 2. Cria o objeto de credenciais usando os valores do User Secrets/appsettings
        var awsCredentials = new BasicAWSCredentials(
            awsSettings["AccessKey"],
            awsSettings["SecretKey"]
        );
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:4200",
                        "https://localhost:4200"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        // 3. Cria as opções da AWS, combinando as credenciais e a região
        var awsOptions = new AWSOptions
        {
            Credentials = awsCredentials,
            Region = Amazon.RegionEndpoint.GetBySystemName(awsSettings["Region"])
        };

        // 4. Registra as opções padrão da AWS no contêiner de DI
        services.AddDefaultAWSOptions(awsOptions);

        // 5. Registra o cliente S3 específico. Ele usará automaticamente as opções que registramos acima.
        services.AddAWSService<IAmazonS3>();

        services.AddSingleton<SceneCompletionListener>();

        services.AddScoped<IClientProfileRepository, ClientProfileRepository>();
        services.AddScoped<IImageUrlRepository, ImageUrlRepository>();
        services.AddScoped<IJobGenerationRepository, JobGenerationRepository>();
        services.AddScoped<IMarketSegmentRepository, MarketSegmentRepository>();
        services.AddScoped<IMidiaProductionRepository, MidiaProductionRepository>();
        services.AddScoped<IObjectiveCampaignRepository, ObjectiveCampaignRepository>();
        services.AddScoped<ITargetAudienceRepository, TargetAudienceRepository>();
        services.AddScoped<IPublishConnectionRepository, PublishConnectionsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddScoped<IAuthenticate, Authenticate>();
        services.AddScoped<IBackgroundScheduler, QuartzJobScheduler>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPromptProcessor, PromptProcessor>();
        services.AddScoped<IStorageService, AwsBucketS3>();
        services.AddScoped<ITextGenerationService, ChatGptTextGenerator>();
        services.AddScoped<IGenerateVideosByScenes, GenerateVideosByScenes>();
        services.AddScoped<IEncryptionService, AesEncryptionService>();
        services.AddScoped<IVideoGenerator, PikaLabsGenerator>();
        services.AddScoped<IMetaApiService, MetaApi>();

        services.AddTransient<ScriptGenerationJob>();
        services.AddTransient<SceneProcessingJob>();
        services.AddTransient<VideoStitchingJob>();

        services.AddQuartz(q =>
        {
            q.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = 10;
            });

            q.UsePersistentStore(store =>
            {
                store.UseClustering(c => c.CheckinInterval = TimeSpan.FromSeconds(10));

                store.UsePostgres(configuration.GetConnectionString("DefaultConnection")!);
                store.UseNewtonsoftJsonSerializer();

            });

            var stitchJobKey = new JobKey("VideoStitchingJob");
            q.AddJob<VideoStitchingJob>(opts => opts.WithIdentity(stitchJobKey).StoreDurably());

            var sp = services.BuildServiceProvider();
            var listener = sp.GetRequiredService<SceneCompletionListener>();
            q.AddJobListener(listener);
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        services.AddHttpContextAccessor();

        services.AddHttpClient();

        var myhandlers = AppDomain.CurrentDomain.Load("Hive.Application");
      
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(myhandlers);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(Hive.Application.Mappings.AssemblyReference).Assembly);

        return services;
    }   
}
