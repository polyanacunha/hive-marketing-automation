using Hive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hive.Infra.Data.EntitiesConfiguration;

public class MidiaProductionConfiguration : IEntityTypeConfiguration<MidiaProduction>
{
    public void Configure(EntityTypeBuilder<MidiaProduction> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(midia => midia.ClientProfile)
              .WithMany(client => client.Midias)
              .HasForeignKey(midia => midia.ClientProfileId);

        builder.HasMany(p => p.InputImageUrl)
               .WithMany(i => i.MidiaProductions);
    }
}
