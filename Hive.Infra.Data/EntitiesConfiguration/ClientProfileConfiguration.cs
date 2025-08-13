using Hive.Domain.Entities;
using Hive.Infra.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hive.Infra.Data.EntitiesConfiguration
{
    public class ClientProfileConfiguration : IEntityTypeConfiguration<ClientProfile>
    {
        public void Configure(EntityTypeBuilder<ClientProfile> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(cp => cp.Id).ValueGeneratedNever();

            builder.Property(p => p.CompanyName)
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(p => p.WebSiteUrl)
            .HasMaxLength(200);

            builder.HasOne<ApplicationUser>()
              .WithOne(u => u.ClientProfile)
              .HasForeignKey<ClientProfile>(c => c.Id);

            builder.HasOne(p => p.MarketSegment)
               .WithMany()
               .HasForeignKey(p => p.MarketSegmentId);

            builder.HasOne(p => p.TargetAudience)
               .WithMany()
               .HasForeignKey(p => p.TargetAudienceId);

            builder.HasMany(p => p.MidiaLinks)
               .WithOne()
               .HasForeignKey(p => p.ClientProfileId);

            builder.HasMany(p => p.PublishConnections)
               .WithOne()
               .HasForeignKey(p => p.ClientProfileId);
        }
    }
}

