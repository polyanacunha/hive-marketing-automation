using Hive.Domain.Entities;
using Hive.Infra.Data.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.EntitiesConfiguration
{
    public class JobGenerationConfiguration : IEntityTypeConfiguration<JobGeneration>
    {
        public void Configure(EntityTypeBuilder<JobGeneration> builder)
        {
            builder.HasKey(p => p.Id);
           
            builder.HasOne(job => job.MidiaProduction)
              .WithMany(client => client.Jobs)
              .HasForeignKey(job => job.MidiaProductionId);
        }
    }
}
