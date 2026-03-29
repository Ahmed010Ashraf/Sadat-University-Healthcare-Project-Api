using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PrescriptionRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public PrescriptionRequestStatus Status { get; set; }

        public string PrescriptionImagePath { get; set; } = null!;
        public string? CommitteeNotes { get; set; }

        public Prescription? Prescription { get; set; }
        public AppUser? User { get; set; } = null!;

    }
}
