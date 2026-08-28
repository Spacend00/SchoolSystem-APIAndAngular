
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<CourseQueryDto>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetAllCoursesQueryHandler(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        public async Task<List<CourseQueryDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var query = _courseRepository.GetAll();
            var courses = await query.Select(c => new CourseQueryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageData = c.ImageData,
                CreatedAt = c.CreatedAt
            }).ToListAsync(cancellationToken);

            return courses;
        }
    }
}
