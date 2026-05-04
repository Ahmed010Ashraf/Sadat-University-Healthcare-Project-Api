using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PrescriptionDispense
{
    public class CreateOrUpdatePrescriptionDispenseDto
    {
        [Required]
        public Guid PrescriptionId { get; set; }

        [Required]
        public Guid PharmacyId { get; set; }

        public DateTime? DispensedAt { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
    }
}
