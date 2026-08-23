
using MediatR;

namespace SchoolSystem.Application.Features.Students.Queries.GetAllActiveStudents
{
    public class GetAllActiveStudentsQuery : IRequest<List<GetAllActiveStudentsDto>>
    {
    }
}
