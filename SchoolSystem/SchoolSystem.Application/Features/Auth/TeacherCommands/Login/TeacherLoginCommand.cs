
using MediatR;

namespace SchoolSystem.Application.Features.Auth.TeacherCommands.Login
{
    public class TeacherLoginCommand : IRequest<TeacherLoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
