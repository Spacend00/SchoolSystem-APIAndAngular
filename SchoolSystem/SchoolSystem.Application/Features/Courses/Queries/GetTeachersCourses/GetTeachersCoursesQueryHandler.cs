
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Courses.Queries.GetTeachersCourses
{
    public class GetTeachersCoursesQueryHandler : IRequestHandler<GetTeachersCoursesQuery, List<CourseQueryDto>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetTeachersCoursesQueryHandler(ICourseRepository courseRepository) => _courseRepository = courseRepository;

        public async Task<List<CourseQueryDto>> Handle(GetTeachersCoursesQuery request, CancellationToken cancellationToken)
        {
            var query = _courseRepository.GetAll().Where(c => c.TeacherId == request.TeacherId);
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
