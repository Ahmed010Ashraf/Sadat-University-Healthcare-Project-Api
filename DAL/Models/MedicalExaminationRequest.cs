using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MedicalExaminationRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }
        public MedicalExaminationRequestType RequestType { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public MedicalExaminationRequestStatus Status { get; set; }

        public string? MedicalReportPath { get; set; }
        public string? CommitteeNotes { get; set; }


        public AppUser? User { get; set; } = null!;

        public MedicalExamination? MedicalExamination { get; set; }
    }
}
