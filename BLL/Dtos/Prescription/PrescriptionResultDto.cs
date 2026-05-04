using DAL.Models.Enums;

namespace BLL.Dtos.Prescription
{
    public class PrescriptionResultDto
    {
        public Guid Id { get; set; }
        public Guid PrescriptionRequestId { get; set; }
        public Guid PrescriptionCode { get; set; }
        public Guid? UserId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public PrescriptionStatus Status { get; set; }
    }
}
