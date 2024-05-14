using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class DoctorToAdd
    {
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
    }
}
