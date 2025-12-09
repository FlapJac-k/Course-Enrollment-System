using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Business.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<Student> CreateStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Guid id);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
    }
}
