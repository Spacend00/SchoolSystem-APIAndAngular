
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Teachers.UpdateTeachers
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand>
    {
        private readonly ITeacherRepository _teacherRepository;
        public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository;
        public async Task Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id);
            if (teacher == null) throw new Exception("Bu Id'ye ait öğretmen bulunamadı.");
            if (!teacher.IsActive) throw new Exception("Bu öğretmen aktif değil.");

            teacher.UpdateProfile(request.Name, request.Surname, request.Age);
            await _teacherRepository.SaveChangesAsync();
        }
    }
}
