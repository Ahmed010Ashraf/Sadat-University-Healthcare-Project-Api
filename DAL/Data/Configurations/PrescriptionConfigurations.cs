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
    public class PrescriptionConfigurations : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasOne(p => p.PrescriptionRequest).WithOne(pr => pr.Prescription).HasForeignKey<Prescription>(p=>p.PrescriptionRequestId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p=>p.PrescriptionDispenses).WithOne(pd=>pd.Prescription).HasForeignKey(pd=>pd.PrescriptionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p=>p.PrescriptionItems).WithOne(pd=>pd.Prescription).HasForeignKey(pd=>pd.PrescriptionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
