namespace BLL.Dtos.PrescriptionDispense
{
    public class PrescriptionDispenseResultDto
    {
        public Guid Id { get; set; }
        public Guid PrescriptionId { get; set; }
        public Guid PharmacyId { get; set; }
        public DateTime DispensedAt { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
