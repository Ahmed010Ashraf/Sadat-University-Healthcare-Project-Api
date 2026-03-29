using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AppUser :IdentityUser<Guid>
    {
        public string FullName { get; set; } = null!;
        public string NationalId { get; set; } = null!;

        public string? UniversityRole { get; set; }
        public Guid? ProviderId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedAt { get; set; }

        public IEnumerable<PrescriptionRequest> PrescriptionRequests { get; set; } 
        public IEnumerable<MedicalExaminationRequest> MedicalExaminationRequests { get; set; } 
        public IEnumerable<Prescription> Prescriptions { get; set; } 
        public IEnumerable<MedicalExamination> MedicalExaminations { get; set; } 

        


    }
}
