
using MediatR;

namespace SchoolSystem.Application.Features.Courses.SoftDeleteCourse
{
    public class SoftDeleteCourseCommand : IRequest
    {
        public Guid Id { get; set; }
        public SoftDeleteCourseCommand(Guid id) => Id = id;
    }
}
