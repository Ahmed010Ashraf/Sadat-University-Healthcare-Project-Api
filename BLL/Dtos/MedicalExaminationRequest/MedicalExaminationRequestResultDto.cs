using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.MedicalExaminationRequest
{
    public class MedicalExaminationRequestResultDto
    {
        public Guid Id { get; set; } 
        public Guid? UserId { get; set; }
        public MedicalExaminationRequestType RequestType { get; set; }
        public DateTime RequestDate { get; set; } 
        public MedicalExaminationRequestStatus Status { get; set; }

        public string? MedicalReportPath { get; set; }
        public string? CommitteeNotes { get; set; }
    }
}
