
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Courses.Queries.GetAllActiveCourses
{
    public class GetAllActiveCoursesQueryHandler : IRequestHandler<GetAllActiveCoursesQuery, List<CourseQueryDto>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetAllActiveCoursesQueryHandler(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        public async Task<List<CourseQueryDto>> Handle(GetAllActiveCoursesQuery request, CancellationToken cancellationToken)
        {
            var query = _courseRepository.GetAll().Where(c => c.IsActive);
            var courses = await query.Select(c => new CourseQueryDto
            {
                Id = c.Id,
                Name = c.Name,
                Credit = c.Credit,
                CreatedAt = c.CreatedAt
            }).ToListAsync(cancellationToken);

            return courses;
        }
    }
}
