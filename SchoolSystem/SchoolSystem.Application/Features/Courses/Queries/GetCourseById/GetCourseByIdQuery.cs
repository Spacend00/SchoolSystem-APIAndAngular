
using MediatR;

namespace SchoolSystem.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<CourseQueryDto>
    {
        public Guid Id { get; set; }
        public GetCourseByIdQuery(Guid id) => Id = id;
    }
}
