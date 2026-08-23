
using FluentValidation.TestHelper;
using SchoolSystem.Application.Features.Auth.StudentCommands.Login;
using System.Diagnostics.Contracts;

namespace SchoolSystem.Application.Tests.Features.Auth.StudentCommands.Login
{
    public class StudentLoginCommanValidatorTests
    {
        private readonly StudentLoginCommandValidator _validator;
        public StudentLoginCommanValidatorTests() 
        {
            _validator = new StudentLoginCommandValidator();
        }
        //<---------Email--------->
        [Fact]
        public void ShouldNotHaveError_WhenEmailIsValid()
        {
            var command = new StudentLoginCommand { Email = "Test@test.com"};
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("Invalid-email")]
        public void ShouldHaveError_WhenEmailIsInvalid(string email)
        {
            var command = new StudentLoginCommand { Email = email };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }
        //<---------Password--------->
        [Fact]
        public void ShouldNotHaveError_WhenPassworIsValid()
        {
            var command = new StudentLoginCommand { Password = "Valid-Password" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Password);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void ShoouldHaveError_WhenPasswordIsInvalid(string password)
        {
            var command = new StudentLoginCommand { Password= password };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Password);
        }
    }
}
