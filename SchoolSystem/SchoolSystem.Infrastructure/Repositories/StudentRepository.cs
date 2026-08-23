
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Persistence;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class StudentRepository : UserEntityRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext appDbContext) : base(appDbContext) { }
        public async Task<bool> ExistsBySchoolNumberAsync(string schoolNumber)
        {
            return await _appDbContext.Students.AnyAsync(x => x.SchoolNumber == schoolNumber);
        }
    }
}
