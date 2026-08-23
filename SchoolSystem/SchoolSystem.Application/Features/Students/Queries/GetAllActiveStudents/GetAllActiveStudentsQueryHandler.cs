
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Students.Queries.GetAllActiveStudents
{
    public class GetAllActiveStudentsQueryHandler : IRequestHandler<GetAllActiveStudentsQuery, List<GetAllActiveStudentsDto>>
    {
        private readonly IStudentRepository _studentRepository;
        public GetAllActiveStudentsQueryHandler(IStudentRepository studentRepository) => _studentRepository = studentRepository;
        public async Task<List<GetAllActiveStudentsDto>> Handle(GetAllActiveStudentsQuery request, CancellationToken cancellationToken)
        {
            var query = _studentRepository.GetAll().Where(x => x.IsActive);
            var students = await query.Select(s => new GetAllActiveStudentsDto
            {
                FullName = $"{s.Name} {s.Surname}",
                Age = s.Age,
                Email = s.Email,
                SchoolNumber = s.SchoolNumber,
                TotalCredit = s.TotalCredit,
                Courses = s.StudentCourses
                .Where(sc => sc.Course != null)
                .Select(sc => sc.Course.Name)
                .ToList()
            }).ToListAsync(cancellationToken);

            return students;
        }
    }
}
