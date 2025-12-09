using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Business.Models;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;

namespace CourseEnrollment.Business.Services
{
    public class EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        ICourseRepository courseRepository,
        IStudentRepository studentRepository
        ) : IEnrollmentService
    {
        public async Task<IEnumerable<StudentCourseEnrollment>> GetAllEnrollmentsAsync()
        {
            return await enrollmentRepository.GetAllAsync();
        }

        public async Task<StudentCourseEnrollment?> GetEnrollmentByIdAsync(Guid id)
        {
            return await enrollmentRepository.GetByIdAsync(id);
        }

        public async Task<EnrollmentResult> EnrollStudentAsync(Guid studentId, Guid courseId)
        {
            var studentExistsTask = studentRepository.GetByIdAsync(studentId);
            var courseExistsTask = courseRepository.GetByIdAsync(courseId);
            var isEnrolledTask = IsStudentEnrolledAsync(studentId, courseId);

            await Task.WhenAll(studentExistsTask, courseExistsTask, isEnrolledTask);

            var student = studentExistsTask.Result;
            var course = courseExistsTask.Result;
            var isEnrolled = isEnrolledTask.Result;

            if (student == null)
            {
                return new EnrollmentResult
                {
                    Success = false,
                    ErrorMessage = "Student not found."
                };
            }

            if (course == null)
            {
                return new EnrollmentResult
                {
                    Success = false,
                    ErrorMessage = "Course not found."
                };
            }

            if (isEnrolled)
            {
                return new EnrollmentResult
                {
                    Success = false,
                    ErrorMessage = "Student is already enrolled in this course."
                };
            }

            if (course.Enrollments.Count >= course.MaximumCapacity)
            {
                return new EnrollmentResult
                {
                    Success = false,
                    ErrorMessage = "Course is full. No available slots."
                };
            }

            var enrollment = new StudentCourseEnrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                CreatedAt = DateTime.UtcNow
            };

            var createdEnrollment = await enrollmentRepository.AddAsync(enrollment);
            return new EnrollmentResult
            {
                Success = true,
                Enrollment = createdEnrollment
            };
        }

        public async Task<bool> IsStudentEnrolledAsync(Guid studentId, Guid courseId)
        {
            return await enrollmentRepository.IsEnrolledAsync(studentId, courseId);
        }

        public async Task<bool> UnenrollStudentAsync(Guid enrollmentId)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(enrollmentId);
            if (enrollment == null) return false;

            await enrollmentRepository.DeleteAsync(enrollmentId);
            return true;
        }
    }
}
