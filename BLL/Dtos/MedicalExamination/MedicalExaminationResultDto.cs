using DAL.Models.Enums;
using System;

namespace BLL.Dtos.MedicalExamination
{
    public class MedicalExaminationResultDto
    {
        public Guid Id { get; set; }
        public Guid MedicalExaminationRequestId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? UserId { get; set; }
        public decimal? Cost { get; set; }
        public MedicalExaminationStatus MedicalExaminationStatus { get; set; }

    }
}
