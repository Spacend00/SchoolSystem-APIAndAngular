
using MediatR;

namespace SchoolSystem.Application.Features.Courses.UpdateCourse
{
    public class UpdateCourseCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? Credit { get; set; }
    }
}
