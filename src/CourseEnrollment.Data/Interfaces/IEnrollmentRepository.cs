using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Data.Interfaces
{
    public interface IEnrollmentRepository : IRepository<StudentCourseEnrollment>
    {
        Task<bool> IsEnrolledAsync(Guid studentId, Guid courseId);
        Task<IEnumerable<StudentCourseEnrollment>> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<StudentCourseEnrollment>> GetByCourseIdAsync(Guid courseId);
    }
}
