
using MediatR;

namespace SchoolSystem.Application.Features.Teachers.UpdateTeachers
{
    public class UpdateTeacherCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Surname { get; set; } = string.Empty;
        public int? Age { get; set; }
    }
}
