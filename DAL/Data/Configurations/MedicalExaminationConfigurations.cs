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
    public class MedicalExaminationConfigurations : IEntityTypeConfiguration<MedicalExamination>
    {
        public void Configure(EntityTypeBuilder<MedicalExamination> builder)
        {
            builder.HasOne(m => m.MedicalExaminationRequest).WithOne(mr => mr.MedicalExamination).HasForeignKey<MedicalExamination>(mr => mr.MedicalExaminationRequestId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
