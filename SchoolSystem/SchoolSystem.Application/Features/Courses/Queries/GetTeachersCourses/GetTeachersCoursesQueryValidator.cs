
using FluentValidation;

namespace SchoolSystem.Application.Features.Courses.Queries.GetTeachersCourses
{
    public class GetTeachersCoursesQueryValidator : AbstractValidator<GetTeachersCoursesQuery>
    {
        public GetTeachersCoursesQueryValidator()
        {
            RuleFor(x => x.TeacherId).NotEmpty().WithMessage("Öğretmen id'si boş olamaz.");
        }
    }
}
