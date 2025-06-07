using Hive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hive.Infra.Data.EntitiesConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Campaing>
{
    public void Configure(EntityTypeBuilder<Campaing> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.UsuarioId);
        builder.Property(p => p.CampaingType).IsRequired();
        builder.Property(p => p.Message);
        builder.Property(p => p.Budget).IsRequired();;
        builder.Property(p => p.InitialDate).IsRequired();;
        builder.Property(p => p.FinalDate).IsRequired();;
        builder.Property(p => p.CampaingStatus).IsRequired();;
        builder.Property(p => p.TargetPublic).IsRequired();;

    }
}
