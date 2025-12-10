using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Business.Models;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using CourseEnrollment.Data.Specifications;

namespace CourseEnrollment.Business.Services
{
    public class CourseService(IUnitOfWork unitOfWork) : ICourseService
    {
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await unitOfWork.Courses.ListAsync(new CourseWithEnrollmentsSpecification());
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await unitOfWork.Courses.FirstOrDefaultAsync(new CourseWithEnrollmentsSpecification(id));
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            ValidateMaxCapacity(course.MaximumCapacity);
            var createdCourse = await unitOfWork.Courses.AddAsync(course);
            await unitOfWork.SaveChangesAsync();
            return createdCourse;
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            ValidateMaxCapacity(course.MaximumCapacity);

            var existingCourse = await unitOfWork.Courses.FirstOrDefaultAsync(new CourseWithEnrollmentsSpecification(course.Id));
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

            await unitOfWork.Courses.UpdateAsync(existingCourse);
            await unitOfWork.SaveChangesAsync();
            return existingCourse;
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await unitOfWork.Courses.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> GetAvailableSlotsAsync(Guid courseId)
        {
            var course = await unitOfWork.Courses.FirstOrDefaultAsync(new CourseWithEnrollmentsSpecification(courseId));
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

            var itemsTask = unitOfWork.Courses.ListAsync(pagedSpec);
            var totalTask = unitOfWork.Courses.CountAsync(new CourseWithEnrollmentsSpecification());

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
