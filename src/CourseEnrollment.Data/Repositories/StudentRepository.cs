using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollment.Data.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
        {
            var query = _context.Set<Student>().Where(s => s.Email == email);
            if (excludeId.HasValue)
            {
                query = query.Where(s => s.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }
    }
}
