
namespace SchoolSystem.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdDto
    {
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string SchoolNumber { get; set; } = string.Empty;
        public int TotalCredit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Courses { get; set; } = new();
    }
}
