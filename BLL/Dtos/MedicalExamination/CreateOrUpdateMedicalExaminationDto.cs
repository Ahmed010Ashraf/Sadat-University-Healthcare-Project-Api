using DAL.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.MedicalExamination
{
    public class CreateOrUpdateMedicalExaminationDto
    {
        [Required]
        public Guid MedicalExaminationRequestId { get; set; }

        [Required]
        public Guid? HospitalId { get; set; }

        public DateTime ExaminationDate { get; set; } = DateTime.UtcNow;

        [Range(0, double.MaxValue)]
        public decimal? Cost { get; set; }

        public MedicalExaminationStatus? MedicalExaminationStatus { get; set; }

    }
}
