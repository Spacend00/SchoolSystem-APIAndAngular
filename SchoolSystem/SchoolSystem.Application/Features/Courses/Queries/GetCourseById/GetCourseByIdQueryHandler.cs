
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, GetCourseByIdDto>
    {
        private readonly ICourseRepository _courseRepository;
        public GetCourseByIdQueryHandler(ICourseRepository courseRepository) => _courseRepository = courseRepository;

        public async Task<GetCourseByIdDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course == null) throw new Exception("Bu id'ye ait kurs bulunamadı.");

            var result = new GetCourseByIdDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                ImageData = course.ImageData,
                Goal = course.Goal,
                Summary = course.Summary,
                TargetGroup = course.TargetGroup,
                Gains = course.Gains,
                Requirements = course.Requirements
            };

            return result;
        }
    }
}
