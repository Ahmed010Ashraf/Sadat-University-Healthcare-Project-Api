using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PharmacyPayment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PharmacyId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        public Pharmacy Pharmacy { get; set; } = null!;
    }
}
