using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Data.Specifications
{
    public class CourseWithEnrollmentsSpecification : Specification<Course>
    {
        public CourseWithEnrollmentsSpecification() : base()
        {
            AddInclude(x => x.Enrollments);
            ApplyOrderBy(x => x.CreatedAt);
        }

        public CourseWithEnrollmentsSpecification(Guid id) : base(x => x.Id == id)
        {
        }

        public CourseWithEnrollmentsSpecification(PagingParams pagingParams) : base()
        {
            AddInclude(x => x.Enrollments);
            ApplyOrderBy(x => x.Title);
            ApplyPaging(pagingParams.PageSize * (pagingParams.PageNumber - 1), pagingParams.PageSize);
        }
    }
}

