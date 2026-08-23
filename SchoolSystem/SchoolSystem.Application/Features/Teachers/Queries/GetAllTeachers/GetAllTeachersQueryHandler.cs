
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetAllTeachers
{
    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, List<TeacherQueryDto>>
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetAllTeachersQueryHandler(ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository;
        public async Task<List<TeacherQueryDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var query = _teacherRepository.GetAll();
            var teachers = await query.Select(t => new TeacherQueryDto
            {
                Id = t.Id,
                FullName = $"{t.Name} {t.Surname}",
                Age = t.Age,
                Email = t.Email,
                IsActive = t.IsActive,
                Branch = t.Branch,
                CreatedAt = t.CreatedAt
            }).ToListAsync(cancellationToken);

            return teachers;
        }
    }
}
