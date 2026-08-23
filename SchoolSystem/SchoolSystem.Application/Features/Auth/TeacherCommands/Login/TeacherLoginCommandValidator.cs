
using FluentValidation;

namespace SchoolSystem.Application.Features.Auth.TeacherCommands.Login
{
    public class TeacherLoginCommandValidator : AbstractValidator<TeacherLoginCommand>
    {
        public TeacherLoginCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.");
        }
    }
}
