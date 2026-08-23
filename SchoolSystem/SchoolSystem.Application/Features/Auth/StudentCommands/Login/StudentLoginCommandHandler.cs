
using MediatR;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Application.Features.Auth.StudentCommands.Login
{
    public class StudentLoginCommandHandler : IRequestHandler<StudentLoginCommand, StudentLoginResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService<IUserEntity> _tokenService;
        public StudentLoginCommandHandler(IStudentRepository studentRepository, IPasswordHasher passwordHasher, ITokenService<IUserEntity> tokenService) 
        {
            _studentRepository = studentRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        public async Task<StudentLoginResponse> Handle(StudentLoginCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByEmailAsync(request.Email);
            if(student == null) throw new UnauthorizedAccessException("E-posta veya şifre yanlış.");

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, student.PasswordHash);
            if (!isPasswordValid) throw new UnauthorizedAccessException("E-posta veya şifre yanlış.");

            if (!student.IsActive) throw new UnauthorizedAccessException("Hesabınız pasife alınmıştır. Lütfen yönetici ile iletişime geçin.");

            var token = await _tokenService.GenerateToken(student);
            return new StudentLoginResponse { Token = token };

        }
    }
}
