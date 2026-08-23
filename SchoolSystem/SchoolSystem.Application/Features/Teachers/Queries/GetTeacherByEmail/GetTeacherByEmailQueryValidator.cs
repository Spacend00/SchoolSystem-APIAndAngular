
using FluentValidation;

namespace SchoolSystem.Application.Features.Teachers.Queries.GetTeacherByEmail
{
    public class GetTeacherByEmailQueryValidator :AbstractValidator<GetTeacherByEmailQuery>
    {
        public GetTeacherByEmailQueryValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
        }
    }
}
