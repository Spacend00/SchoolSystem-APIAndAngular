
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Persistence;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class TeacherRepository : UserEntityRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
