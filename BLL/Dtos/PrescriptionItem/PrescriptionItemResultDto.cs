namespace BLL.Dtos.PrescriptionItem
{
    public class PrescriptionItemResultDto
    {
        public Guid Id { get; set; }
        public Guid PrescriptionId { get; set; }
        public string MedicineName { get; set; } = null!;
        public int QuantityApproved { get; set; }
        public string? UnitType { get; set; }
        public string? Notes { get; set; }
    }
}
