
namespace SchoolSystem.Application.Features.Courses.Queries
{
    public class CourseQueryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Credit { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
