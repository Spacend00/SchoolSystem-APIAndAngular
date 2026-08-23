
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Application.Common.Interfaces
{
    public interface ITokenService<T> where T : IUserEntity
    {
        Task<string> GenerateToken(T entity);
    }
}
