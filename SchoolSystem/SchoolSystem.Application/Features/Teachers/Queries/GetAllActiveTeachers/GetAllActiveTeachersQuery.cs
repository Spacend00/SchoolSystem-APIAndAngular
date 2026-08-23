
using MediatR;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetAllActiveTeachers
{
    public class GetAllActiveTeachersQuery : IRequest<List<TeacherQueryDto>>
    {
    }
}
