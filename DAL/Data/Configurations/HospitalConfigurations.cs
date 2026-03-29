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
    public class HospitalConfigurations : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasMany(h=>h.HospitalPayments).WithOne(hp=>hp.Hospital).HasForeignKey(hp=>hp.HospitalId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h=>h.MedicalExaminations).WithOne(hp=>hp.Hospital).HasForeignKey(hp=>hp.HospitalId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
