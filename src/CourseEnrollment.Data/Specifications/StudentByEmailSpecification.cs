using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Data.Specifications
{
    public class StudentByEmailSpecification : Specification<Student>
    {
        public StudentByEmailSpecification(string email, Guid? excludeId = null) : base(x =>
            (x.Email == email) &&
            (!excludeId.HasValue || x.Id != excludeId)
        )
        {
        }
    }
}

