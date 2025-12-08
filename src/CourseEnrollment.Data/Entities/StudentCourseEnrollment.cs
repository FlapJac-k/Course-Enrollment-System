namespace CourseEnrollment.Data.Entities
{
    public class StudentCourseEnrollment
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
