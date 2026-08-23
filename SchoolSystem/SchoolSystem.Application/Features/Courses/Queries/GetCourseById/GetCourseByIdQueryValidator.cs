
using FluentValidation;

namespace SchoolSystem.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryValidator : AbstractValidator<GetCourseByIdQuery>
    {
        public GetCourseByIdQueryValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
