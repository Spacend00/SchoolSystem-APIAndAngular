
using MediatR;

namespace SchoolSystem.Application.Features.Courses.UpdateCourse
{
    public class UpdateCourseCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageData { get; set; }
        public string? Goal { get; set; }
        public string? Summary { get; set; }
        public string? TargetGroup { get; set; }
        public string? Gains { get; set; }
        public string? Requirements { get; set; }
    }
}
