using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PrescriptionItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PrescriptionId { get; set; }
        public string MedicineName { get; set; } = null!;
        public int QuantityApproved { get; set; }
        public string? UnitType { get; set; }
        public string? Notes { get; set; }

        public Prescription Prescription { get; set; } = null!;
        public ICollection<PrescriptionDispenseItem> PrescriptionDispenseItems { get; set; } 

    }
}
