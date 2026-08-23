
using FluentValidation;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQueryValidator : AbstractValidator<GetTeacherByIdQuery>
    {
        public GetTeacherByIdQueryValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
