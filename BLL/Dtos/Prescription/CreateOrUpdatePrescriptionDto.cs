using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.Prescription
{
    public class CreateOrUpdatePrescriptionDto
    {
        [Required]
        public Guid PrescriptionRequestId { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Active;
    }
}
