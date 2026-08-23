
using FluentValidation;

namespace SchoolSystem.Application.Features.Auth.StudentCommands.Login
{
    public class StudentLoginCommandValidator : AbstractValidator<StudentLoginCommand>
    {
        public StudentLoginCommandValidator() 
        {
            RuleFor(lc => lc.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");

            RuleFor(lc => lc.Password).NotEmpty().WithMessage("Şifre boş olamaz.");
        }
    }
}
