
using MediatR;
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Application.Features.Auth.TeacherCommands.Register
{
    public class TeacherRegisterCommand : IRequest<TeacherRegisterResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int Age { get; set; }
        public Branch Branch { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
