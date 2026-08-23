
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Domain.Interfaces
{
    public interface IUserEntity : IEntity
    {
        string Email { get; }
        Role Role { get; }
    }
}
