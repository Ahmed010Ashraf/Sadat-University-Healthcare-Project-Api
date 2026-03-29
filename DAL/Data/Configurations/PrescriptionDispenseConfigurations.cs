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
    public class PrescriptionDispenseConfigurations : IEntityTypeConfiguration<PrescriptionDispense>
    {
        public void Configure(EntityTypeBuilder<PrescriptionDispense> builder)
        {
            builder.HasMany(p=>p.PrescriptionDispenseItems)
                .WithOne(pt=>pt.PrescriptionDispense)
                .HasForeignKey(pt=>pt.PrescriptionDispenseId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
