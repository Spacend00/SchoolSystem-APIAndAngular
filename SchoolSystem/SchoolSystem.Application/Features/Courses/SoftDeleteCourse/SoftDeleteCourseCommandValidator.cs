
using FluentValidation;

namespace SchoolSystem.Application.Features.Courses.SoftDeleteCourse
{
    public class SoftDeleteCourseCommandValidator : AbstractValidator<SoftDeleteCourseCommand>
    {
        public SoftDeleteCourseCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
