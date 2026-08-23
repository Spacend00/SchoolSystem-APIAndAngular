
using FluentValidation.TestHelper;
using SchoolSystem.Application.Features.Auth.TeacherCommands.Register;
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Application.Tests.Features.Auth.TeacherCommand.Register
{
    public class TeacherRegisterCommandValidatorTests
    {
        private readonly TeacherRegisterCommandValidator _validator;
        public TeacherRegisterCommandValidatorTests() 
        {
            _validator = new TeacherRegisterCommandValidator();
        }
        //<----------Name---------->
        [Fact]
        public void ShouldNotHaveError_WhenNameIsValid()
        {
            var command = new TeacherRegisterCommand { Name = "Hatice"};
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("Deniz123")]
        [InlineData("123.deniz")]
        [InlineData("Deniz@")]
        public void ShouldHaveErrpr_WhenNameIsInvalid(string name)
        {
            var command = new TeacherRegisterCommand { Name = name };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
        //<----------Surname---------->
        [Fact]
        public void ShouldNotHaveError_WhenSurnameIsValid()
        {
            var command = new TeacherRegisterCommand { Surname = "Derya" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Surname);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Derya123")]
        [InlineData("123.derya")]
        [InlineData("Derya@")]
        public void ShouldHaveError_WhenSurnameIsInvalid(string surname)
        {
            var command = new TeacherRegisterCommand { Surname = surname };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Surname);
        }
        //<----------Age---------->
        [Theory]
        [InlineData(20)]
        [InlineData(130)]
        public void ShouldNotHaveError_WhenAgeInRange(int age)
        {
            var command = new TeacherRegisterCommand { Age = age };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Age);
        }
        [Theory]
        [InlineData(-30)]
        [InlineData(19)]
        [InlineData(131)]
        public void ShouldHaveError_WhenAgeIsOutOfRange(int age)
        {
            var command = new TeacherRegisterCommand { Age = age };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Age);
        }
        //<----------Email---------->
        [Fact]
        public void ShouldNotHaveError_WhenEmailIsValid()
        {
            var command = new TeacherRegisterCommand { Email = "Teacher_123@test.com" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Test@")]
        [InlineData("test.com")]
        public void ShouldHaveError_WhenEmailIsInvalid(string email)
        {
            var command = new TeacherRegisterCommand { Email = email };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }
        //<----------Password---------->
        [Fact]
        public void ShouldNotHaveError_WhenPasswordIsValid()
        {
            var command = new TeacherRegisterCommand { Password = "Test_1234" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Password);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Test_12")]
        [InlineData("TestTest")]
        [InlineData("test1234")]
        public void ShouldHaveError_WhenPasswordIsInvalid(string password)
        {
            var command = new TeacherRegisterCommand { Password = password };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Password);
        }
        [Fact]
        public void ShouldNotHaveError_WhenBranchIsValid()
        {
            var command = new TeacherRegisterCommand { Branch = Branch.Coğrafya };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Branch);
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(33)]
        [InlineData(999)]
        public void ShouldHaveError_WhenBranchIsInvalid(int branch)
        {
            var command = new TeacherRegisterCommand { Branch = (Branch)branch };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Branch);
        }
    }
}
