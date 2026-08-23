
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Students.DeleteStudents
{
    public class SoftDeleteStudentCommandHandler : IRequestHandler<SoftDeleteStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public SoftDeleteStudentCommandHandler(IStudentRepository studentRepository) => _studentRepository = studentRepository;
        public async Task Handle(SoftDeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student == null) throw new InvalidOperationException("Öğrenci bulunamadı.");
            if (!student.IsActive) throw new Exception("Bu öğrenci zaten aktif değil.");

            student.Deactivate();
            await _studentRepository.SaveChangesAsync();
        }
    }
}
