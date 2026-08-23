
using MediatR;

namespace SchoolSystem.Application.Features.Students.DeleteStudents
{
    public class SoftDeleteStudentCommand : IRequest
    {
        public Guid Id { get; set; }
        public SoftDeleteStudentCommand(Guid id) => Id = id;
    }
}
