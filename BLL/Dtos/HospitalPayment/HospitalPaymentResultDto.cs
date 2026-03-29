using System;

namespace BLL.Dtos.HospitalPayment
{
    public class HospitalPaymentResultDto
    {
        public Guid Id { get; set; }
        public Guid HospitalId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
