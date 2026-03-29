using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MedicalExamination
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MedicalExaminationRequestId { get; set; }
        public Guid? HospitalId { get; set; }

        public Guid? UserId { get; set; }
        public DateTime ExaminationDate { get; set; } = DateTime.UtcNow;
        public decimal? Cost { get; set; }

        public MedicalExaminationStatus MedicalExaminationStatus { get; set; }

        public MedicalExaminationRequest MedicalExaminationRequest { get; set; } = null!;
        public AppUser? User { get; set; }
        public Hospital Hospital { get; set; } = null!;
    }
}
