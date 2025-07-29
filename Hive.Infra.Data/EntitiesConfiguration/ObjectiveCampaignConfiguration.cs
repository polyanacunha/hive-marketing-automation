using Hive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.EntitiesConfiguration
{
    public class ObjectiveCampaignConfiguration : IEntityTypeConfiguration<ObjectiveCampaign>
    {
        public void Configure(EntityTypeBuilder<ObjectiveCampaign> builder)
        {
            builder.Property(p => p.Name)
            .HasConversion<string>();
        }
    }
}
