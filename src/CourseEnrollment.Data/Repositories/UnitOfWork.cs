using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Interfaces;

namespace CourseEnrollment.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Student>? _students;
        private IRepository<Course>? _courses;
        private IRepository<StudentCourseEnrollment>? _enrollments;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Student> Students
        {
            get
            {
                _students ??= new Repository<Student>(_context);
                return _students;
            }
        }

        public IRepository<Course> Courses
        {
            get
            {
                _courses ??= new Repository<Course>(_context);
                return _courses;
            }
        }

        public IRepository<StudentCourseEnrollment> Enrollments
        {
            get
            {
                _enrollments ??= new Repository<StudentCourseEnrollment>(_context);
                return _enrollments;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

