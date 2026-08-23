
using FluentValidation;

namespace SchoolSystem.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQueryValidator : AbstractValidator<GetStudentByIdQuery>
    {
        public GetStudentByIdQueryValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
