
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Teachers.SoftDeleteTeachers
{
    public class SoftDeleteTeacherCommandHandler : IRequestHandler<SoftDeleteTeacherCommand>
    {
        private readonly ITeacherRepository _teacherRepository;
        public SoftDeleteTeacherCommandHandler(ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository; 
        public async Task Handle(SoftDeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id);
            if (teacher == null) throw new Exception("Bu Id'ye ait öğretmen bulunamadı.");

            if (!teacher.IsActive) throw new Exception("Bu öğretmen zaten silinmiş.");

            teacher.Deactivate();
            await _teacherRepository.SaveChangesAsync();
        }
    }
}
