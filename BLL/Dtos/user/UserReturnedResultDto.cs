using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.user
{
    public class UserReturnedResultDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string NationalId { get; set; } = null!;

        public string? UniversityRole { get; set; }
        public Guid? ProviderId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? ApprovedAt { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
