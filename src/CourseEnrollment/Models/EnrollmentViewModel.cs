using System.ComponentModel.DataAnnotations;

namespace CourseEnrollment.Models
{
    public class EnrollmentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Student is required")]
        [Display(Name = "Student")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [Display(Name = "Course")]
        public Guid CourseId { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public required string StudentName { get; set; }
        public required string CourseTitle { get; set; }
        public required int AvailableSlots { get; set; }
    }
}
