using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Hospital
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<MedicalExamination> MedicalExaminations { get; set; } = new List<MedicalExamination>();
        public ICollection<HospitalPayment> HospitalPayments { get; set; } = new List<HospitalPayment>();
    }
}
