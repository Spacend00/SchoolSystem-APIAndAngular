
using FluentValidation.TestHelper;
using SchoolSystem.Application.Features.Students.UpdateStudents;

namespace SchoolSystem.Application.Tests.Features.Students.Update
{
    public class UpdateStudentCommandValidatorTests
    {
        private readonly UpdateStudentCommandValidator _validator;
        public UpdateStudentCommandValidatorTests()
        {
            _validator = new UpdateStudentCommandValidator();
        }
        //<-----------Id----------->
        [Fact]
        public void ShouldNotHaveError_WhenIdIsValid()
        {
            var command = new UpdateStudentCommand { Id = Guid.NewGuid() };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Id);
        }
        [Fact]
        public void ShouldHaveError_WhenIdIsInvalid()
        {
            var command = new UpdateStudentCommand { Id = Guid.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }
        //<-----------Name----------->
        [Fact]
        public void ShouldNotHaveError_WhenNameIsValid()
        {
            var command = new UpdateStudentCommand { Name = "Fatih" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
        }
        [Theory]
        [InlineData("Fatih123")]
        [InlineData("123_fatih")]
        [InlineData("fatih123teke")]
        public void ShouldHaveError_WhenNameIsInvalid(string name)
        {
            var command = new UpdateStudentCommand { Name = name };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
        //<-----------Surname----------->
        [Fact]
        public void ShouldNotHaveError_WhenSurnameIsValid()
        {
            var command = new UpdateStudentCommand { Surname = "Gökçen" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Surname);
        }
        [Theory]
        [InlineData("Sabiha_")]
        [InlineData("Sabiha_123")]
        [InlineData("Sabiha123gökçen")]
        public void ShouldHaveError_WhenSurnameIsInvalid(string surname)
        {
            var command = new UpdateStudentCommand { Surname = surname };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Surname);
        }
        //<-----------Age----------->
        [Theory]
        [InlineData(15)]
        [InlineData(130)]
        public void ShouldNotHaveError_WhenAgeIsInRange(int age)
        {
            var command = new UpdateStudentCommand { Age = age };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Age);
        }
        [Theory]
        [InlineData(-55)]
        [InlineData(14)]
        [InlineData(131)]
        public void ShouldHaveError_WhenAgeIsOutOfRange(int age)
        {
            var command = new UpdateStudentCommand { Age = age };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Age);
        }
    }
}
