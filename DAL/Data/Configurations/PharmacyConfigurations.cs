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
    public class PharmacyConfigurations : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.HasMany(p=>p.PharmacyPayments).WithOne(pp=>pp.Pharmacy).HasForeignKey(pp=>pp.PharmacyId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p=>p.PrescriptionDispenses).WithOne(pp=>pp.Pharmacy).HasForeignKey(pp=>pp.PharmacyId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
