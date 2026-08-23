
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, GetStudentByIdDto>
    {
        private readonly IStudentRepository _studentRepository;
        public GetStudentByIdQueryHandler(IStudentRepository studentRepository) => _studentRepository = studentRepository;
        public async Task<GetStudentByIdDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student == null) throw new Exception("Bu id'ye ait öğrenci bulunamadı.");

            var result = new GetStudentByIdDto
            {
                FullName = $"{student.Name} {student.Surname}",
                Age = student.Age,
                Email = student.Email,
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
