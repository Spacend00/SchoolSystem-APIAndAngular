
using FluentValidation;

namespace SchoolSystem.Application.Features.Students.Queries.GetStudentByEmail
{
    public class GetStudentByEmailQueryValidator : AbstractValidator<GetStudentByEmailQuery>
    {
        public GetStudentByEmailQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-poasta adresi giriniz.");
        }
    }
}
