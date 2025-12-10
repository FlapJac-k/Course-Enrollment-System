using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using CourseEnrollment.Data.Specifications;

namespace CourseEnrollment.Business.Services
{
    public class StudentService(IUnitOfWork unitOfWork) : IStudentService
    {
        public async Task<Student> CreateStudentAsync(Student student)
        {
            if (await IsEmailUniqueAsync(student.Email))
            {
                var createdStudent = await unitOfWork.Students.AddAsync(student);
                await unitOfWork.SaveChangesAsync();
                return createdStudent;
            }
            throw new InvalidOperationException($"A student with email '{student.Email}' already exists.");
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id)
        {
            return await unitOfWork.Students.GetByIdAsync(id);
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            if (!await IsEmailUniqueAsync(student.Email, student.Id))
            {
                throw new InvalidOperationException($"A student with email '{student.Email}' already exists.");
            }

            var existingStudent = await unitOfWork.Students.GetByIdAsync(student.Id);
            if (existingStudent == null)
            {
                throw new InvalidOperationException($"Student with ID {student.Id} not found.");
            }

            existingStudent.FullName = student.FullName;
            existingStudent.Email = student.Email;
            existingStudent.Birthdate = student.Birthdate;
            existingStudent.NationalId = student.NationalId;
            existingStudent.PhoneNumber = student.PhoneNumber;

            await unitOfWork.Students.UpdateAsync(existingStudent);
            await unitOfWork.SaveChangesAsync();
            return existingStudent;
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var spec = new EnrollmentWithDetailsSpecification(studentId: id);
            var enrollments = await unitOfWork.Enrollments.ListAsync(spec);
            
            if (enrollments.Any())
            {
                await unitOfWork.Enrollments.DeleteRangeAsync(enrollments);
            }
            
            await unitOfWork.Students.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            var spec = new StudentByEmailSpecification(email, excludeId);
            var existingCount = await unitOfWork.Students.CountAsync(spec);
            return existingCount == 0;
        }
    }
}
