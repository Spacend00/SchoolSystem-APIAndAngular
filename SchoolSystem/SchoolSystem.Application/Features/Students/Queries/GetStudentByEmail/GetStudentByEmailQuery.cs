
using MediatR;

namespace SchoolSystem.Application.Features.Students.Queries.GetStudentByEmail
{
    public class GetStudentByEmailQuery : IRequest<GetStudentByEmailDto>
    {
        public string Email { get; set; } = string.Empty;
        public GetStudentByEmailQuery(string email) => Email = email;
    }
}
