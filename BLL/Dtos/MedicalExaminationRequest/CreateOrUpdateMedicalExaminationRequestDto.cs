using DAL.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.MedicalExaminationRequest
{
    public class CreateOrUpdateMedicalExaminationRequestDto
    {
        public MedicalExaminationRequestType RequestType { get; set; }
        public IFormFile? MedicalReportPath { get; set; }
        public string? CommitteeNotes { get; set; }
        public MedicalExaminationRequestStatus? Status { get; set; }

    }
}
