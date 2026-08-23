
using MediatR;
using SchoolSystem.Application.Common.Interfaces;

namespace SchoolSystem.Application.Features.Students.UpdateStudents
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public UpdateStudentCommandHandler(IStudentRepository studentRepository) => _studentRepository = studentRepository;

        public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student == null) throw new FileNotFoundException("Öğrenci bulunamadı.");
            if (!student.IsActive) throw new Exception("Bu öğrenci aktif değil.");

            student.UpdateProfile(request.Name, request.Surname, request.Age);
            await _studentRepository.SaveChangesAsync();
        }
    }
}
