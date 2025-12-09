namespace CourseEnrollment.Data.Entities
{
    public class Student : BaseEntity
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required DateTime Birthdate { get; set; }
        public required string NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public List<StudentCourseEnrollment> Enrollments { get; set; } = new List<StudentCourseEnrollment>();
    }
}