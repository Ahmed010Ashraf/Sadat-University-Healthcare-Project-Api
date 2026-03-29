using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "NationalId is required")]
        public string NationalId { get; set; } = null!;

        [Required(ErrorMessage = "UniversityRole is required")]

        public string UniversityRole { get; set; }
    }
}
