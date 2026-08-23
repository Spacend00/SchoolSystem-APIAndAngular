
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Persistence;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class CourseRepository : GeneralRepository<Course> , ICourseRepository
    {
        public CourseRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
