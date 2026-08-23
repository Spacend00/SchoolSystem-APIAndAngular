
using MediatR;

namespace SchoolSystem.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<GetStudentByIdDto>
    {
        public Guid Id { get; set; }
        public GetStudentByIdQuery(Guid id) => Id = id;
    }
}
