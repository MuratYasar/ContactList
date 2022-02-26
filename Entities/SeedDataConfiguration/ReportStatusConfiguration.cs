using Entities.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SeedDataConfiguration
{
    public class ReportStatusConfiguration : IEntityTypeConfiguration<ReportStatus>
    {
        public void Configure(EntityTypeBuilder<ReportStatus> builder)
        {
            builder.ToTable("ReportStatus", "public");
            builder.Property(s => s.Name).IsRequired(true);
            builder.Property(s => s.DateCreated).HasDefaultValue<DateTime>(DateTime.UtcNow);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.HasData(
                new DataModel.ReportStatus { Id = 1, Name = "Hazırlanıyor" },
                new DataModel.ReportStatus { Id = 2, Name = "Tamamlandı" }
            );
        }
    }
}
