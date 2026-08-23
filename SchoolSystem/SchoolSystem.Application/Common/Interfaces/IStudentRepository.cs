
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Application.Common.Interfaces
{
    public interface IStudentRepository : IUserEntityRepository<Student>
    {        
        Task<bool> ExistsBySchoolNumberAsync(string schoolNumber);
    }
}
