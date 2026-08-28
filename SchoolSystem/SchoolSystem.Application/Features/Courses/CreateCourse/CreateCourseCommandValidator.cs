
using FluentValidation;

namespace SchoolSystem.Application.Features.Courses.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.TeacherId).NotEmpty().WithMessage("Öğretmen id'si boş olamaz");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Kurs ismi maximum 50 karakter olmalıdır.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Tanım alanı boş olamaz.");
            RuleFor(x => x.TargetGroup).NotEmpty().WithMessage("Hedef grup alanı boş olamaz");
            RuleFor(x => x.Requirements).NotEmpty().WithMessage("Gerekliler alanı boş olamaz");
            RuleFor(x => x.Summary).NotEmpty().WithMessage("Özet alanı boş olamaz.");
            RuleFor(x => x.Gains).NotEmpty().WithMessage("Kazınımlar alını boş olamaz.");
            RuleFor(x => x.Goal).NotEmpty().WithMessage("Hedef alanı boş olamaz.");
            RuleFor(x => x.ImageData).NotEmpty().WithMessage("Resim alanı boş olamaz.");
        }
    }
}
