using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PrescriptionDispenseItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PrescriptionDispenseId { get; set; }
        public Guid PrescriptionItemId { get; set; }

        public string MedicineName { get; set; } = null!;
        public int QuantityDispensed { get; set; }
        public decimal UnitPrice { get; set; }

        public PrescriptionDispense PrescriptionDispense { get; set; } = null!;
        public PrescriptionItem PrescriptionItem { get; set; } = null!;

    }
}
