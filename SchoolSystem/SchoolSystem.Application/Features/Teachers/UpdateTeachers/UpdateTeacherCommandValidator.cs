
using FluentValidation;

namespace SchoolSystem.Application.Features.Teachers.UpdateTeachers
{
    public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Ad maximum 50 karakter olmalıdır.");
            RuleFor(x => x.Surname).MaximumLength(50).WithMessage("Soyad maximum 50 karakter olmalıdır.");
            RuleFor(x => x.Age).InclusiveBetween(20, 130).WithMessage("Yaş 20 - 130 arasında olmalıdır.");
        }
    }
}
