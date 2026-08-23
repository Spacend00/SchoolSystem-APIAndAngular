
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetTeacherByEmail
{
    public class GetTeacherByEmailQueryHandler : IRequestHandler<GetTeacherByEmailQuery, TeacherQueryDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetTeacherByEmailQueryHandler(ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository;
        public async Task<TeacherQueryDto> Handle(GetTeacherByEmailQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByEmailAsync(request.Email);
            if (teacher == null) throw new Exception("Öğretmen mevcut değil veya e-posta yanlış.");

            var result = new TeacherQueryDto
            {
                Id = teacher.Id,
                FullName = $"{teacher.Name} {teacher.Surname}",
                Age = teacher.Age,
                Email = teacher.Email,
                IsActive = teacher.IsActive,
                Branch = teacher.Branch,
                CreatedAt = teacher.CreatedAt
            };

            return result;
        }
    }
}
