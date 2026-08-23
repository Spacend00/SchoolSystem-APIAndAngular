
using MediatR;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetAllTeachers
{
    public class GetAllTeachersQuery : IRequest<List<TeacherQueryDto>>
    {
    }
}
