
using FluentValidation.TestHelper;
using SchoolSystem.Application.Features.Students.DeleteStudents;

namespace SchoolSystem.Application.Tests.Features.Students.Delete
{
    public class SoftDeleteStudentCommandValidatorTests
    {
        private readonly SoftDeleteStudentCommandValidator _validator;
        public SoftDeleteStudentCommandValidatorTests() 
        {
            _validator = new SoftDeleteStudentCommandValidator();
        }
        [Fact]
        public void ShouldNotHaveError_WhenIdIsValid()
        {
            Guid id = Guid.NewGuid();
            var command = new SoftDeleteStudentCommand(id);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Id);
        }
        [Fact]
        public void ShouldHaveError_WhenIdIsInvalid()
        {
            Guid id = Guid.Empty;
            var command = new SoftDeleteStudentCommand(id);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }
    }
}
