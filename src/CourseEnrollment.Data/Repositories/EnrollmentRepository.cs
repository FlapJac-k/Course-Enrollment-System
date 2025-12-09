using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollment.Data.Repositories
{
    public class EnrollmentRepository : Repository<StudentCourseEnrollment>, IEnrollmentRepository
    {

        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<StudentCourseEnrollment?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public override async Task<IEnumerable<StudentCourseEnrollment>> GetAllAsync()
        {
            return await _dbSet
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourseEnrollment>> GetByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Include(e => e.Student)
                .Where(e => e.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourseEnrollment>> GetByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<bool> IsEnrolledAsync(Guid studentId, Guid courseId)
        {
            return await _dbSet
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
