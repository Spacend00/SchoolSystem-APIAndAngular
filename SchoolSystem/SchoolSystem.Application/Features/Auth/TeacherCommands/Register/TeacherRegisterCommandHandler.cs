
using MediatR;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Application.Features.Auth.TeacherCommands.Register
{
    public class TeacherRegisterCommandHandler : IRequestHandler<TeacherRegisterCommand, TeacherRegisterResponse>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPasswordHasher _passwordHasher;
        public TeacherRegisterCommandHandler(ITeacherRepository teacherRepository, IPasswordHasher passwordHasher)
        {
            _teacherRepository = teacherRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<TeacherRegisterResponse> Handle(TeacherRegisterCommand request, CancellationToken cancellationToken)
        {
            bool exists = await _teacherRepository.ExistsByEmailAsync(request.Email);
            if (exists) throw new InvalidOperationException("Bu e-posta zaten kayıtlı.");

            string passwordHash = _passwordHasher.HashPassword(request.Password);
            var teacher = new Teacher(request.Name, request.Surname, request.Age, request.Email, passwordHash, request.Branch);
            await _teacherRepository.CreateAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            return new TeacherRegisterResponse { Id = teacher.Id, Email = teacher.Email };
        }
    }
}
