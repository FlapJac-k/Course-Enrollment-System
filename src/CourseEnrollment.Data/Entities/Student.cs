namespace CourseEnrollment.Data.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required DateTime Birthdate { get; set; }
        public required string NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public List<StudentCourseEnrollment> Enrollments { get; set; } = new List<StudentCourseEnrollment>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}