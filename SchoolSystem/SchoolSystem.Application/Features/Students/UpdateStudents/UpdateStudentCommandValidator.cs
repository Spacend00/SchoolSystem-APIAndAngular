
using FluentValidation;

namespace SchoolSystem.Application.Features.Students.UpdateStudents
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");

            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Ad 50 karakteri geçmemelidir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Ad alanı sayı veya özel karakter içeremez.")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Surname)
                .MaximumLength(50).WithMessage("Soyad 50 karakteri geçmemelidir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Ad alanı sayı veya özel karakter içeremez.")
                .When(x => !string.IsNullOrWhiteSpace(x.Surname));

            RuleFor(x => x.Age)
                .InclusiveBetween(15, 130).WithMessage("Öğrenci yaşı 15 - 130 arasında olmalıdır.")
                .When(x => x.Age.HasValue);
        }
    }
}
