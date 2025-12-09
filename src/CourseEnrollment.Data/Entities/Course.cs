namespace CourseEnrollment.Data.Entities
{
    public class Course : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int MaximumCapacity { get; set; }
        public List<StudentCourseEnrollment> Enrollments { get; set; } = new List<StudentCourseEnrollment>();
    }
}
