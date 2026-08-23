
using MediatR;

namespace SchoolSystem.Application.Features.Teachers.SoftDeleteTeachers
{
    public class SoftDeleteTeacherCommand : IRequest
    {
        public Guid Id { get; set; }
        public SoftDeleteTeacherCommand(Guid id) => Id = id;
    }
}
