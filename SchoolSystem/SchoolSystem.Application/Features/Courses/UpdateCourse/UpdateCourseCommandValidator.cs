
using FluentValidation;

namespace SchoolSystem.Application.Features.Courses.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Kurs adı maximum 50 karakter olmalıdır.");
        }
    }
}
