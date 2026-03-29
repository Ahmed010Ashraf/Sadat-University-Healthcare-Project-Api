using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos.Hospital
{
    public class CreateOrUpdateHospitalDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
