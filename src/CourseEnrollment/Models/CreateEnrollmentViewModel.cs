using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseEnrollment.Models
{
    public class CreateEnrollmentViewModel
    {
        [Required(ErrorMessage = "Student is required")]
        [Display(Name = "Student")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [Display(Name = "Course")]
        public Guid CourseId { get; set; }

        public List<SelectListItem> Students { get; set; } = new();
        public List<SelectListItem> Courses { get; set; } = new();
    }
}
