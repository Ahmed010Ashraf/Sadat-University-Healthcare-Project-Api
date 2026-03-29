using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PrescriptionDispense
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PrescriptionId { get; set; }
        public Guid PharmacyId { get; set; }
                                                                        
        public DateTime DispensedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        public Prescription Prescription { get; set; } = null!;
        public Pharmacy Pharmacy { get; set; } = null!;

        public ICollection<PrescriptionDispenseItem> PrescriptionDispenseItems { get; set; } = new List<PrescriptionDispenseItem>();
    }
}
