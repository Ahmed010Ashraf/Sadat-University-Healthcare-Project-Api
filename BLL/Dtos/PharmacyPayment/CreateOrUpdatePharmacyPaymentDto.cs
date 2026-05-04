using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.PharmacyPayment
{
    public class CreateOrUpdatePharmacyPaymentDto
    {
        [Required]
        public Guid PharmacyId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}
