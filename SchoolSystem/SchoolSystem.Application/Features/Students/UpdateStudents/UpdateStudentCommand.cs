
using MediatR;

namespace SchoolSystem.Application.Features.Students.UpdateStudents
{
    public class UpdateStudentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Surname { get; set; } = null;
        public int? Age { get; set; }
    }
}
