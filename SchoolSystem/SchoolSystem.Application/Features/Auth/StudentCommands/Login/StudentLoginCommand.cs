
using MediatR;

namespace SchoolSystem.Application.Features.Auth.StudentCommands.Login
{
    public class StudentLoginCommand : IRequest<StudentLoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
