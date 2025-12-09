using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollment.Data.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Enrollments)
                .ToListAsync();
        }
    }
}
