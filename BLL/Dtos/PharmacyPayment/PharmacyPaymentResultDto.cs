namespace BLL.Dtos.PharmacyPayment
{
    public class PharmacyPaymentResultDto
    {
        public Guid Id { get; set; }
        public Guid PharmacyId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
