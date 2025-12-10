using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using CourseEnrollment.Data.Specifications;

namespace CourseEnrollment.Business.Services
{
    public class StudentService(IRepository<Student> studentRepository) : IStudentService
    {
        public async Task<Student> CreateStudentAsync(Student student)
        {
            if (await IsEmailUniqueAsync(student.Email))
            {
                return await studentRepository.AddAsync(student);
            }
            throw new InvalidOperationException($"A student with email '{student.Email}' already exists.");
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await studentRepository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await studentRepository.GetByIdAsync(id);
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            if (!await IsEmailUniqueAsync(student.Email, student.Id))
            {
                throw new InvalidOperationException($"A student with email '{student.Email}' already exists.");
            }

            var existingStudent = await studentRepository.GetByIdAsync(student.Id);
            if (existingStudent == null)
            {
                throw new InvalidOperationException($"Student with ID {student.Id} not found.");
            }

            existingStudent.FullName = student.FullName;
            existingStudent.Email = student.Email;
            existingStudent.Birthdate = student.Birthdate;
            existingStudent.NationalId = student.NationalId;
            existingStudent.PhoneNumber = student.PhoneNumber;

            await studentRepository.UpdateAsync(existingStudent);
            return existingStudent;
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            await studentRepository.DeleteAsync(id);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            var spec = new StudentByEmailSpecification(email, excludeId);
            var existingCount = await studentRepository.CountAsync(spec);
            return existingCount == 0;
        }
    }
}
