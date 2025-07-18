using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using Hive.Infra.Data.Identity;
using Hive.Infra.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hive.Infra.Data.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    private readonly IEncryptionService _encryptionService;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IEncryptionService encryptionService)
        : base(options)
    {
        _encryptionService = encryptionService;
    }

    public DbSet<ClientProfile> ClientProfile { get; set; }
    public DbSet<MarketSegment> MarketSegment { get; set; }
    public DbSet<TargetAudience> TargetAudience { get; set; }
    public DbSet<MidiaProduction> MidiaProduction { get; set; }
    public DbSet<JobGeneration> JobGeneration { get; set; }
    public DbSet<ImageUrl> ImageUrl { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<ObjectiveCampaign> ObjectiveCampaigns { get; set; }
    public DbSet<PublishConnection> PublishConnections { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);
        SeedRoles(builder);
        SeedUsers(builder);
        SeedUserRoles(builder);

        var converter = new EncryptedStringConverter(_encryptionService);

        builder.Entity<PublishConnection>()
               .Property(p => p.AccessToken)
               .HasConversion(converter);
    }
    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "a18be9c0-aa65-4af8-bd17-002f23242000",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "a18be9c0-aa65-4af8-bd17-002f23242001",
                Name = "Client",
                NormalizedName = "CLIENT"
            }
        );
    }
    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var adminUserId = "a18be9c0-aa65-4af8-bd17-002f23242002";

        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Numsey#2021"),
                SecurityStamp = Guid.NewGuid().ToString()
            }
        );
    }
    private static void SeedUserRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "a18be9c0-aa65-4af8-bd17-002f23242000", 
                UserId = "a18be9c0-aa65-4af8-bd17-002f23242002" 
            }
        );
    }
}
