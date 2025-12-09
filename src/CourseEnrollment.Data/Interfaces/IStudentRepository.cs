using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Data.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
    }
}
