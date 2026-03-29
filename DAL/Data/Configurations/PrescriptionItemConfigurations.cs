using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations
{
    public class PrescriptionItemConfigurations : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.HasMany(pt => pt.PrescriptionDispenseItems)
                .WithOne(pd => pd.PrescriptionItem)
                .HasForeignKey(pd => pd.PrescriptionItemId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
