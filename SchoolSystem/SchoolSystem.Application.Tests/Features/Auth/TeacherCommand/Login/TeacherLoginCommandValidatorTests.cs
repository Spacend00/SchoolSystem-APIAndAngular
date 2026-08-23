
using FluentValidation.TestHelper;
using SchoolSystem.Application.Features.Auth.TeacherCommands.Login;

namespace SchoolSystem.Application.Tests.Features.Auth.TeacherCommand.Login
{
    public class TeacherLoginCommandValidatorTests
    {
        private readonly TeacherLoginCommandValidator _validator;
        public TeacherLoginCommandValidatorTests() 
        {
            _validator = new TeacherLoginCommandValidator();
        }
        //<----------Email---------->
        [Fact]
        public void ShouldNotHaveError_WhenEmailIsValid()
        {
            var command = new TeacherLoginCommand { Email = "Test_123@test.com" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }
        [Theory]
        [InlineData("")]
        [InlineData("test.com")]
        [InlineData("test123@")]
        [InlineData("12_test")]
        public void ShouldHaveError_WhenEmailIsInvalid(string email)
        {
            var command = new TeacherLoginCommand { Email = email };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }
        //<----------Password---------->
        [Fact]
        public void ShouldNotHaveError_WhenPasswordIsValid()
        {
            var command = new TeacherLoginCommand { Password = "Test_123" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Password);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldHaveError_WhenPasswordIsInvalid(string password) 
        {
            var command = new TeacherLoginCommand { Password = password };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Password);
        }
    }
}
