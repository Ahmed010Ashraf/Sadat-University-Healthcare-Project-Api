using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.HospitalPayment
{
    public class CreateOrUpdateHospitalPaymentDto
    {
        [Required]
        public Guid HospitalId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}
