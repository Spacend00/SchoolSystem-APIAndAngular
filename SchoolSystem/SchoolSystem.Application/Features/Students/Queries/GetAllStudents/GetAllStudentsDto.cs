
namespace SchoolSystem.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsDto
    {
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string SchoolNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
