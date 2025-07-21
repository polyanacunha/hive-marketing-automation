using Hive.Domain.Entities;
using Hive.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace Hive.Infra.Data.EntitiesConfiguration
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {


            builder.HasKey(p => p.Id);

            builder.OwnsOne(campaign => campaign.PeriodRange, periodBuilder =>
            {
                periodBuilder.Property(p => p.Initial).HasColumnName("Period_InitialDate");
                periodBuilder.Property(p => p.End).HasColumnName("Period_EndDate");
            });

            builder.OwnsOne(c => c.Budget, budget =>
            {
                budget.Property(b => b.Value).HasColumnName("BudgetValue");
                budget.Property(b => b.Currency).HasColumnName("BudgetCurrency").HasMaxLength(3);
            });
        }
    }
}
