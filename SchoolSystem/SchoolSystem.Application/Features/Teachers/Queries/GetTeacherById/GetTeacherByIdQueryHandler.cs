
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, TeacherQueryDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetTeacherByIdQueryHandler(ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository;
        public async Task<TeacherQueryDto> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id);
            if (teacher == null) throw new Exception("Bu Id'ye ait öğretmen bulunamadı.");
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
