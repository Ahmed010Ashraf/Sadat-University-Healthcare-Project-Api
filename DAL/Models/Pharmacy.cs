using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Pharmacy
    {
        public Guid Id { get; set; }= Guid.NewGuid();

        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PrescriptionDispense> PrescriptionDispenses { get; set; } = new List<PrescriptionDispense>();
        public ICollection<PharmacyPayment> PharmacyPayments { get; set; } = new List<PharmacyPayment>();
    }
}
