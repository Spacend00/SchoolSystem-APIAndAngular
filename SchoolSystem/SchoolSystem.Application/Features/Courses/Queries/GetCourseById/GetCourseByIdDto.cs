
namespace SchoolSystem.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageData { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string TargetGroup { get; set; } = string.Empty;
        public string Gains { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
    }
}
