using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Business.Models;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using CourseEnrollment.Data.Specifications;

namespace CourseEnrollment.Business.Services
{
    public class EnrollmentService(
        IRepository<StudentCourseEnrollment> enrollmentRepository,
        IRepository<Course> courseRepository,
        IRepository<Student> studentRepository
        ) : IEnrollmentService
    {
        public async Task<IEnumerable<StudentCourseEnrollment>> GetAllEnrollmentsAsync()
        {
            return await enrollmentRepository.ListAsync(new EnrollmentWithDetailsSpecification());
        }

        public async Task<StudentCourseEnrollment?> GetEnrollmentByIdAsync(Guid id)
        {
            return await enrollmentRepository.FirstOrDefaultAsync(new EnrollmentWithDetailsSpecification(id: id));
        }

        public async Task<EnrollmentResult> EnrollStudentAsync(Guid studentId, Guid courseId)
        {
            var studentExistsTask = studentRepository.GetByIdAsync(studentId);
            var courseExistsTask = courseRepository.FirstOrDefaultAsync(new CourseWithEnrollmentsSpecification(courseId));
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
            var spec = new EnrollmentWithDetailsSpecification(courseId: courseId, studentId: studentId);
            var count = await enrollmentRepository.CountAsync(spec);
            return count > 0;
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
