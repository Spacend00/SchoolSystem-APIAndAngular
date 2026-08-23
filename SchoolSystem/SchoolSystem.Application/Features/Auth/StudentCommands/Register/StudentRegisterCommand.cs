
using MediatR;

namespace SchoolSystem.Application.Features.Auth.StudentCommands.Register
{
    public class StudentRegisterCommand : IRequest<StudentRegisterResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string SchoolNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
