
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Interfaces;
using SchoolSystem.Infrastructure.Persistence;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class, IEntity
    {
        protected readonly AppDbContext _appDbContext;
        public GeneralRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;
        public async Task CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }      
        public IQueryable<T> GetAll()
        {
            return _appDbContext.Set<T>().AsNoTracking();
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
