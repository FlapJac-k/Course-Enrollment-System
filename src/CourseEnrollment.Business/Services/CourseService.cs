using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;

namespace CourseEnrollment.Business.Services
{
    public class CourseService(ICourseRepository courseRepository) : ICourseService
    {
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await courseRepository.GetAllAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await courseRepository.GetByIdAsync(id);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            ValidateMaxCapacity(course.MaximumCapacity);
            return await courseRepository.AddAsync(course);
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            ValidateMaxCapacity(course.MaximumCapacity);

            var existingCourse = await courseRepository.GetByIdAsync(course.Id);
            if (existingCourse == null)
            {
                throw new InvalidOperationException($"Course with ID {course.Id} not found.");
            }

            if (course.MaximumCapacity < existingCourse.Enrollments.Count)
            {
                throw new InvalidOperationException($"Maximum capacity cannot be less than current enrollments ({existingCourse.Enrollments.Count}).");
            }

            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.MaximumCapacity = course.MaximumCapacity;

            await courseRepository.UpdateAsync(existingCourse);
            return existingCourse;
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await courseRepository.DeleteAsync(id);
        }

        public async Task<int> GetAvailableSlotsAsync(Guid courseId)
        {
            var course = await courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return 0;
            }
            return course.MaximumCapacity - course.Enrollments.Count;
        }


        private void ValidateMaxCapacity(int maxCapacity)
        {
            if (maxCapacity <= 0)
            {
                throw new ArgumentException("Maximum capacity must be greater than 0.");
            }
        }
    }
}
