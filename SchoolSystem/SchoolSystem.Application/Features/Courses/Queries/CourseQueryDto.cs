
namespace SchoolSystem.Application.Features.Courses.Queries
{
    public class CourseQueryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageData { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
