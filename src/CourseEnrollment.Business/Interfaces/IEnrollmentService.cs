using CourseEnrollment.Business.Models;
using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Business.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<StudentCourseEnrollment>> GetAllEnrollmentsAsync();
        Task<StudentCourseEnrollment?> GetEnrollmentByIdAsync(Guid id);
        Task<EnrollmentResult> EnrollStudentAsync(Guid studentId, Guid courseId);
        Task<bool> UnenrollStudentAsync(Guid enrollmentId);
        Task<bool> IsStudentEnrolledAsync(Guid studentId, Guid courseId);
    }
}
