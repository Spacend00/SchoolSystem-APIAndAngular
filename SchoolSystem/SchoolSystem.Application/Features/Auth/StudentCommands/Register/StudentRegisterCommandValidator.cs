
using FluentValidation;

namespace SchoolSystem.Application.Features.Auth.StudentCommands.Register
{
    public class StudentRegisterCommandValidator : AbstractValidator<StudentRegisterCommand>
    {
        public StudentRegisterCommandValidator() 
        {
            RuleFor(rc => rc.Name)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad 50 karakteri geçmemelidir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Ad alanı sayı veya özel karakter içeremez.");

            RuleFor(rc => rc.Surname)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad 50 karakteri geçmemelidir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Soyad alanı sayı veya özel karakter içeremez.");

            RuleFor(rc => rc.Age)
                .InclusiveBetween(15, 130).WithMessage("Öğrenci yaşı 15 - 130 arasında olmalıdır.");

            RuleFor(rc => rc.SchoolNumber)
                .NotEmpty().WithMessage("Okul numarası boş olamaz.")
                .MaximumLength(20).WithMessage("Okul numarası 20 karakteri geçmemelidir.");

            RuleFor(rc => rc.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");

            RuleFor(rc => rc.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");
        }
    }
}
