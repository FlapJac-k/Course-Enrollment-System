using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Business.Models;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using CourseEnrollment.Data.Specifications;

namespace CourseEnrollment.Business.Services
{
    public class CourseService(IRepository<Course> courseRepository) : ICourseService
    {
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await courseRepository.ListAsync(new CourseWithEnrollmentsSpecification());
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await courseRepository.FirstOrDefaultAsync(new CourseWithEnrollmentsSpecification(id));
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            ValidateMaxCapacity(course.MaximumCapacity);
            return await courseRepository.AddAsync(course);
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            ValidateMaxCapacity(course.MaximumCapacity);

            var existingCourse = await courseRepository.FirstOrDefaultAsync(new CourseWithEnrollmentsSpecification(course.Id));
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
            var course = await courseRepository.FirstOrDefaultAsync(new CourseWithEnrollmentsSpecification(courseId));
            if (course == null)
            {
                return 0;
            }
            return course.MaximumCapacity - course.Enrollments.Count;
        }

        public async Task<PagedResult<Course>> GetPagedCourseAsync(int pageNumber, int pageSize)
        {
            var pageParam = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
            var pagedSpec = new CourseWithEnrollmentsSpecification(pageParam);

            var itemsTask = courseRepository.ListAsync(pagedSpec);
            var totalTask = courseRepository.CountAsync(new CourseWithEnrollmentsSpecification());

            await Task.WhenAll(itemsTask, totalTask);

            return new PagedResult<Course>(
                itemsTask.Result,
                totalTask.Result,
                pageNumber,
                pageSize
            );
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
