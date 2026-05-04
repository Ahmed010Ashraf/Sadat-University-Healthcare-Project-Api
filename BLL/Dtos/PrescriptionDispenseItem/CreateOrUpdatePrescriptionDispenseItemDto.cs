using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PrescriptionDispenseItem
{
    public class CreateOrUpdatePrescriptionDispenseItemDto
    {
        [Required]
        public Guid PrescriptionDispenseId { get; set; }

        [Required]
        public Guid PrescriptionItemId { get; set; }

        [Required]
        public string MedicineName { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityDispensed { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}
