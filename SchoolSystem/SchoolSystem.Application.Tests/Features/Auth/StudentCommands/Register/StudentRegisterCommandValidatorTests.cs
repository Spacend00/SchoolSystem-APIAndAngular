
using FluentValidation.TestHelper;
using SchoolSystem.Application.Features.Auth.StudentCommands.Register;

namespace SchoolSystem.Application.Tests.Features.Auth.StudentCommands.Register
{
    public class StudentRegisterCommandValidatorTests
    {
        private readonly StudentRegisterCommandValidator _validator;
        public StudentRegisterCommandValidatorTests()
        {
            _validator = new StudentRegisterCommandValidator();
        }
        //<--------Name-------->
        [Fact]
        public void ShouldNotHaveError_WhenNameIsValid()
        {
            var command = new StudentRegisterCommand { Name = "Fatma" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Ahmet123")]
        [InlineData("Salih3demir")]
        [InlineData("1234")]
        public void ShouldHaveError_WhenNameIsInvalid(string name)
        {
            var command = new StudentRegisterCommand { Name = name };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
        //<--------Surname-------->
        [Fact]
        public void ShouldNotHaveError_WhenSurnameIsValid()
        {
            var command = new StudentRegisterCommand { Name = "Timur" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Reyhan123")]
        [InlineData("Salih3demir")]
        [InlineData("1234")]
        public void ShouldHaveError_WhenSurnameIsInvalid(string surname)
        {
            var command = new StudentRegisterCommand { Name = surname };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Surname);
        }
        //<--------Age-------->
        [Theory]
        [InlineData(15)]
        [InlineData(130)]
        public void ShouldNotHaveError_WhenAgeIsInRange(int age)
        {
            var command = new StudentRegisterCommand { Age = age };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Age);
        }
        [Theory]
        [InlineData(-22)]
        [InlineData(14)]
        [InlineData(131)]
        public void ShouldHaveError_WhenAgeIsOutOfRange(int age)
        {
            var command = new StudentRegisterCommand { Age = age };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Age);
        }
        //<--------SchoolNumber-------->
        [Fact]
        public void ShouldNotHaveError_WhenSchoolNumberIsValid()
        {
            var command = new StudentRegisterCommand { SchoolNumber = "sc-1234" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.SchoolNumber);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldHaveError_WhenSchoolNumberIsInvalid(string schoolNumber)
        {
            var command = new StudentRegisterCommand { SchoolNumber = schoolNumber };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.SchoolNumber);
        }
        //<--------Email-------->
        [Fact]
        public void ShouldNotHaveError_WhenEmailIsValid()
        {
            var command = new StudentRegisterCommand { Email = "Test@test.com" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Invalid-email")]
        public void ShouldHaveError_WhenEmailIsInvalid(string email)
        {
            var command = new StudentRegisterCommand { Email = email };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }
        //<--------Password-------->
        [Theory]
        [InlineData("1234Test")]
        [InlineData("Deneme12")]
        [InlineData("tEST_123")]
        public void ShouldNotHaveError_WhenPasswordIsValid(string password)
        {
            var command = new StudentRegisterCommand { Password = password };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Password);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("1Test")]
        [InlineData("test1234")]
        [InlineData("TEST1234")]
        [InlineData("TEST.test")]
        public void ShouldHaveError_WhenPasswordIsInvalid(string password)
        {
            var command = new StudentRegisterCommand { Password= password };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Password);
        }
    }
}
