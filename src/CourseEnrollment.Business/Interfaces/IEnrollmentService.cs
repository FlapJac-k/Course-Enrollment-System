using CourseEnrollment.Business.Models;
using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Business.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<StudentCourseEnrollment>> GetAllEnrollmentsAsync();
        Task<StudentCourseEnrollment?> GetEnrollmentByIdAsync(int id);
        Task<EnrollmentResult> EnrollStudentAsync(int studentId, int courseId);
        Task<bool> UnenrollStudentAsync(int enrollmentId);
        Task<IEnumerable<StudentCourseEnrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<IEnumerable<StudentCourseEnrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<bool> IsStudentEnrolledAsync(int studentId, int courseId);
        Task<bool> IsCourseFullAsync(int courseId);
    }
}
