
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Students.Queries.GetStudentByEmail
{
    public class GetStudentByEmailQueryHandler : IRequestHandler<GetStudentByEmailQuery, GetStudentByEmailDto>
    {
        private readonly IStudentRepository _studentRepository;
        public GetStudentByEmailQueryHandler(IStudentRepository studentRepository) => _studentRepository = studentRepository;
        public async Task<GetStudentByEmailDto> Handle(GetStudentByEmailQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByEmailAsync(request.Email);
            if (student == null) throw new Exception("E-poasta adresi yanlış veya kayıtlı değil.");

            var result = new GetStudentByEmailDto 
            {
                FullName = $"{student.Name} {student.Surname}",
                Age = student.Age,
                Email = request.Email,
                SchoolNumber = student.SchoolNumber,
                TotalCredit = student.TotalCredit,
                IsActive = student.IsActive,
                CreatedAt = student.CreatedAt,
                Courses = student.StudentCourses.Where(sc => sc.Course != null).Select(sc => sc.Course.Name).ToList()
            };

            return result;
        }
    }
}
