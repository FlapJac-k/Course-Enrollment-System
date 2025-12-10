using CourseEnrollment.Data.Entities;
using CourseEnrollment.Data.Specifications;

namespace CourseEnrollment.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
