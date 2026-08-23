
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Application.Common.Interfaces
{
    public interface IUserEntityRepository<T> : IGeneralRepository<T> where T : IUserEntity
    {
        Task<T?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
