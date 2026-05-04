namespace BLL.Dtos.PrescriptionDispenseItem
{
    public class PrescriptionDispenseItemResultDto
    {
        public Guid Id { get; set; }
        public Guid PrescriptionDispenseId { get; set; }
        public Guid PrescriptionItemId { get; set; }
        public string MedicineName { get; set; } = null!;
        public int QuantityDispensed { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
