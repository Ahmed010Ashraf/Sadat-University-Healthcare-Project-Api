using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PrescriptionItem
{
    public class CreateOrUpdatePrescriptionItemDto
    {
        [Required]
        public Guid PrescriptionId { get; set; }

        [Required]
        public string MedicineName { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityApproved { get; set; }

        public string? UnitType { get; set; }

        public string? Notes { get; set; }
    }
}
