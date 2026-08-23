
using FluentValidation;

namespace SchoolSystem.Application.Features.Auth.TeacherCommands.Register
{
    public class TeacherRegisterCommandValidator : AbstractValidator<TeacherRegisterCommand>
    {
        public TeacherRegisterCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad 50 karakteri geçmemelidir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Ad alanı sayı veya özel karakter içeremez.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad 50 karakteri geçmemelidir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Soyad alanı sayı veya özel karakter içeremez.");

            RuleFor(x => x.Age)
                .InclusiveBetween(20, 130).WithMessage("Öğretmen yaşı 20 - 130 arasında olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakterden oluşmalıdır.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");

            RuleFor(x => x.Branch)
                .IsInEnum().WithMessage("Geçerli bir branş giriniz");
        }
    }
}
