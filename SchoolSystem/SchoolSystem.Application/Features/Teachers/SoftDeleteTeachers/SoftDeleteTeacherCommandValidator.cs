
using FluentValidation;

namespace SchoolSystem.Application.Features.Teachers.SoftDeleteTeachers
{
    public class SoftDeleteTeacherCommandValidator : AbstractValidator<SoftDeleteTeacherCommand>
    {
        public SoftDeleteTeacherCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
