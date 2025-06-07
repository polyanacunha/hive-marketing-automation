using Hive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hive.Infra.Data.EntitiesConfiguration;

public class MidiaConfiguration : IEntityTypeConfiguration<Midia>
{
    public void Configure(EntityTypeBuilder<Midia> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Duration).IsRequired();
        builder.Property(m => m.AspectRatio);
        builder.Property(m => m.Url);
    }
}
