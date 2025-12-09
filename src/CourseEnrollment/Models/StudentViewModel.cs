using System.ComponentModel.DataAnnotations;

namespace CourseEnrollment.Models
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "National ID is required")]
        [StringLength(14, ErrorMessage = "National ID must be maximum 14 characters")]
        [Display(Name = "National ID")]
        public required string NationalId { get; set; }

        [StringLength(11, ErrorMessage = "Phone number must be maximum 11 characters")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
