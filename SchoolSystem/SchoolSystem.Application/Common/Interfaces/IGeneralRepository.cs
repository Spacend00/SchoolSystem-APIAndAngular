
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Application.Common.Interfaces
{
    public interface IGeneralRepository<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(Guid id);        
        Task CreateAsync(T entity);
        IQueryable<T> GetAll();        
        Task SaveChangesAsync();
    }
}
