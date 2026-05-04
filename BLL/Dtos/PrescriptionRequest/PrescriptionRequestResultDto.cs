using DAL.Models.Enums;

namespace BLL.Dtos.PrescriptionRequest
{
    public class PrescriptionRequestResultDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public PrescriptionRequestStatus Status { get; set; }
        public string PrescriptionImagePath { get; set; } = null!;
        public string? CommitteeNotes { get; set; }
    }
}
