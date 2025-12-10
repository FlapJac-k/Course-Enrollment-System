using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Data.Specifications
{
    public class EnrollmentWithDetailsSpecification : Specification<StudentCourseEnrollment>
    {
        public EnrollmentWithDetailsSpecification() : base()
        {
            AddInclude(x => x.Student);
            AddInclude(x => x.Course);
            ApplyOrderBy(x => x.CreatedAt);
        }

        public EnrollmentWithDetailsSpecification(Guid? id = null, Guid? courseId = null, Guid? studentId = null) : base(x =>
            (!id.HasValue || x.Id == id) &&
            (!courseId.HasValue || x.CourseId == courseId) &&
            (!studentId.HasValue || x.StudentId == studentId)
        )
        {
            AddInclude(x => x.Student);
            AddInclude(x => x.Course);
            ApplyOrderBy(x => x.CreatedAt);
        }
    }
}

