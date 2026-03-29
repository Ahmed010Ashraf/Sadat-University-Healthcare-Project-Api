using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Prescription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PrescriptionRequestId { get; set; }
        public Guid PrescriptionCode { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public PrescriptionStatus Status { get; set; }


        public PrescriptionRequest PrescriptionRequest { get; set; } = null!;
        public AppUser? User { get; set; } = null!;

        public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
        public ICollection<PrescriptionDispense> PrescriptionDispenses { get; set; } = new List<PrescriptionDispense>();
    }
}
