using CourseEnrollment.Business.Models;
using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Business.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(Guid id);
        Task<Course> CreateCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(Guid id);
        Task<int> GetAvailableSlotsAsync(Guid courseId);
        Task<PagedResult<Course>> GetPagedCourseAsync(int pageNumber, int pageSize);

    }
}
