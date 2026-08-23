
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetAllActiveTeachers
{
    public class GetAllActiveTeachersQueryHandler : IRequestHandler<GetAllActiveTeachersQuery, List<TeacherQueryDto>>
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetAllActiveTeachersQueryHandler(ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository;
        public async Task<List<TeacherQueryDto>> Handle(GetAllActiveTeachersQuery request, CancellationToken cancellationToken)
        {
            var quary = _teacherRepository.GetAll().Where(t => t.IsActive);
            var teachers = await quary.Select(t => new TeacherQueryDto
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
