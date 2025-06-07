using Hive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hive.Infra.Data.EntitiesConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Url).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Legenda);
        builder.Property(p => p.Url);
    }
}
