
using MediatR;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Application.Features.Auth.StudentCommands.Register
{
    public class StudentRegisterCommandHandler : IRequestHandler<StudentRegisterCommand, StudentRegisterResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IPasswordHasher _passwordHasher;
        public StudentRegisterCommandHandler(IStudentRepository studentRepository, IPasswordHasher passwordHasher)
        {
            _studentRepository = studentRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<StudentRegisterResponse> Handle(StudentRegisterCommand request, CancellationToken cancellationToken)
        {
            bool existsByEmail = await _studentRepository.ExistsByEmailAsync(request.Email);
            if (existsByEmail) throw new InvalidOperationException("Bu e-posta zaten kayıtlı.");

            bool existsBySchoolNumber = await _studentRepository.ExistsBySchoolNumberAsync(request.SchoolNumber);
            if (existsBySchoolNumber) throw new InvalidOperationException("Bu okul numarası zaten kayıtlı.");

            string passwordHash = _passwordHasher.HashPassword(request.Password);

            var student = new Student(request.Name, request.Surname, request.Age, request.SchoolNumber, request.Email, passwordHash);

            await _studentRepository.CreateAsync(student);
            await _studentRepository.SaveChangesAsync();

            return new StudentRegisterResponse { Id = student.Id, Email = student.Email };
        }
    }
}
