
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Application.Features.Teachers.Queries
{
    public class TeacherQueryDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Branch Branch { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
