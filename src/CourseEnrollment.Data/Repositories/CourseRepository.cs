using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollment.Data.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public override async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Course>()
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Set<Course>()
                .Include(c => c.Enrollments)
                .ToListAsync();
        }
    }
}
