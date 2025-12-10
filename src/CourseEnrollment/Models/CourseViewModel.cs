using System.ComponentModel.DataAnnotations;

namespace CourseEnrollment.Models
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Maximum capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximum capacity must be at least 1")]
        [Display(Name = "Maximum Capacity")]
        public required int MaximumCapacity { get; set; }

        public int AvailableSlots { get; set; }
        public int EnrollmentCount { get; set; }
    }
}
