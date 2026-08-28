
using MediatR;

namespace SchoolSystem.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<GetCourseByIdDto>
    {
        public Guid Id { get; set; }
        public GetCourseByIdQuery(Guid id) => Id = id;
    }
}
