
using MediatR;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Application.Features.Auth.TeacherCommands.Login
{
    public class TeacherLoginCommandHandler : IRequestHandler<TeacherLoginCommand, TeacherLoginResponse>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService<IUserEntity> _tokenService;
        public TeacherLoginCommandHandler(ITeacherRepository teacherRepository, IPasswordHasher passwordHasher, ITokenService<IUserEntity> tokenService)
        {
            _teacherRepository = teacherRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        public async Task<TeacherLoginResponse> Handle(TeacherLoginCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByEmailAsync(request.Email);
            if (teacher == null) throw new UnauthorizedAccessException("E-posta veya şifre yanlış.");

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, teacher.PasswordHash);
            if (!isPasswordValid) throw new UnauthorizedAccessException("E-posta veya şifre yanlış.");

            if (!teacher.IsActive) throw new UnauthorizedAccessException("Hesabınız pasife alınmıştır. Lütfen yönetici ile iletişime geçin.");

            string token = await _tokenService.GenerateToken(teacher);
            return new TeacherLoginResponse { Token = token };
        }
    }
}
