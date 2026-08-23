
using MediatR;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetTeacherByEmail
{
    public class GetTeacherByEmailQuery : IRequest<TeacherQueryDto>
    {
        public string Email { get; set; } = string.Empty;
        public GetTeacherByEmailQuery(string email) => Email = email;
    }
}
