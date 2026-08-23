
using MediatR;

namespace SchoolSystem.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsQuery : IRequest<List<GetAllStudentsDto>>
    {
    }
}
