
using MediatR;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQuery : IRequest<TeacherQueryDto>
    {
        public Guid Id { get; set; }
        public GetTeacherByIdQuery(Guid id) => Id = id;

    }
}
