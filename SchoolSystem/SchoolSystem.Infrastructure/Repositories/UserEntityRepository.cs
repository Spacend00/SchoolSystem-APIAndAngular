
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class UserEntityRepository<T> : GeneralRepository<T>, IUserEntityRepository<T> where T : class, IUserEntity
    {
        public UserEntityRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _appDbContext.Set<T>().AnyAsync(x => x.Email == email);
        }
        public async Task<T?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
