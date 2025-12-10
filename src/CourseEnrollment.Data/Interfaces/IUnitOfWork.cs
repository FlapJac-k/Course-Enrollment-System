using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<Course> Courses { get; }
        IRepository<StudentCourseEnrollment> Enrollments { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

