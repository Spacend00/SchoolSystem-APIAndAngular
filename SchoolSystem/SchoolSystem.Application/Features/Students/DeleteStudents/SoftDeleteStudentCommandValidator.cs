
using FluentValidation;

namespace SchoolSystem.Application.Features.Students.DeleteStudents
{
    public class SoftDeleteStudentCommandValidator : AbstractValidator<SoftDeleteStudentCommand>
    {
        public SoftDeleteStudentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
