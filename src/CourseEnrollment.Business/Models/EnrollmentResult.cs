using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Business.Models
{
    public class EnrollmentResult
    {
        public bool Success { get; set; }
        public StudentCourseEnrollment? Enrollment { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
