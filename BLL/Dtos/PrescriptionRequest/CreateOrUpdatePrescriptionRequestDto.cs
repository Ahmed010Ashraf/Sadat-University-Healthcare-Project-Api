using DAL.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace BLL.Dtos.PrescriptionRequest
{
    public class CreateOrUpdatePrescriptionRequestDto
    {
        public IFormFile? PrescriptionImage { get; set; }
        public string? CommitteeNotes { get; set; }
        public PrescriptionRequestStatus? Status { get; set; }
    }
}
