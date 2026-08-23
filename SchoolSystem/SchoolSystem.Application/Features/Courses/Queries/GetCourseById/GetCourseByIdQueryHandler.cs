
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseQueryDto>
    {
        private readonly ICourseRepository _courseRepository;
        public GetCourseByIdQueryHandler(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        public async Task<CourseQueryDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course == null) throw new Exception("Bu id'ye ait kurs bulunamadı.");

            var result = new CourseQueryDto
            {
                Id = course.Id,
                Name = course.Name,
                Credit = course.Credit,
                CreatedAt = course.CreatedAt
            };

            return result;
        }
    }
}
