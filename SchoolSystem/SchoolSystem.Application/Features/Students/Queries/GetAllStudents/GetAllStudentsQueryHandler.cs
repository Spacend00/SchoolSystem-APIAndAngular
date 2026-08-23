
using MediatR;
using SchoolSystem.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<GetAllStudentsDto>>
    {
        private readonly IStudentRepository _studentRepository;
        public GetAllStudentsQueryHandler(IStudentRepository studentRepository) => _studentRepository = studentRepository;

        public async Task<List<GetAllStudentsDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var query = _studentRepository.GetAll();
            var students = await query
                .Select(s => new GetAllStudentsDto
                {
                    FullName = $"{s.Name} {s.Surname}",
                    Age = s.Age,
                    Email = s.Email,
                    SchoolNumber = s.SchoolNumber,
                    IsActive = s.IsActive
                }).ToListAsync(cancellationToken);

            return students;
        }
    }
}
