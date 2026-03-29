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
    public class UserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u=>u.Prescriptions).WithOne(p=>p.User).HasForeignKey(p=>p.UserId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(u=>u.MedicalExaminations).WithOne(m=>m.User).HasForeignKey(m=>m.UserId).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.MedicalExaminationRequests).WithOne(m => m.User).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(u => u.PrescriptionRequests).WithOne(p => p.User).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
