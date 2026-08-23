
namespace SchoolSystem.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
        string Name { get; }
        bool IsActive { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
