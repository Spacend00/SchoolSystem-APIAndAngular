
using FluentAssertions;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Domain.Tests.Entities
{
    public class StudentTests
    {
        private Student CreateValidStudent(
            string name = "Ahmet",
            string surname = "Demir",
            int age = 18,
            string schoolNumber = "S-3455083-ng",
            string email = "AhmetDemir@Test.com",
            string passwordHash = "hashed_password_123"
            )
        {
            return new Student(name, surname, age, schoolNumber, email, passwordHash);
        }
        //<----------For Constructor---------->
        [Fact]
        public void Constructor_ShouldCreateStudent_WhenAllDataIsValid()
        {
            //Arrange
            var student = CreateValidStudent();
            //Act

            //Assert
            student.Id.Should().NotBe(Guid.Empty);
            student.Name.Should().Be("Ahmet");
            student.Surname.Should().Be("Demir");
            student.Age.Should().Be(18);
            student.SchoolNumber.Should().Be("S-3455083-ng");
            student.Email.Should().Be("AhmetDemir@Test.com");
            student.PasswordHash.Should().Be("hashed_password_123");
            student.TotalCredit.Should().Be(32);
            student.Role.Should().Be(Role.Student);
            student.IsActive.Should().BeTrue();
            student.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            student.UpdatedAt.Should().Be(null);

        }
        [Theory]
        [InlineData(15)]
        [InlineData(130)]
        public void Constructor_ShouldThrowException_WhenAgeIsOutOfRange(int invalidAge)
        {
            var act = () => new Student("Ahmet", "Demir", invalidAge, "S-3455083-ng", "AhmetDemir@Test.com", "hashed_password_123");
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsEmpty() 
        {
            var act = () => new Student("", "Demir", 20, "S-3455083-ng", "AhmetDemir@Test.com", "hashed_password_123");            
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenSurnameIsEmpty()
        {
            var act = () => new Student("Ahmet", "", 20, "S-3455083-ng", "AhmetDemir@Test.com", "hashed_password_123");
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenSchoolNumberIsEmpty()
        {
            var act = () => new Student("Ahmet", "Demir", 20, "", "AhmetDemir@Test.com", "hashed_password_123");
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenEmailIsEmpty()
        {
            var act = () => new Student("Ahmet", "Demir", 20, "S-3455083-ng", "", "hashed_password_123");
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_ShouldThrowException_WhenPasswordHashIsEmpty()
        {
            var act = () => new Student("Ahmet", "Demir", 20, "S-3455083-ng", "AhmetDemir@Test.com", "");
            act.Should().Throw<ArgumentException>();
        }
        //<----------For IsActive---------->
        [Fact]
        public void Deactivate_WhenCalled_ShouldIsActiveFalseAndAssingUpdatedAt()
        {
            //Arrange
            var student = CreateValidStudent();
            //Act
            student.Deactivate();
            //Assert
            student.IsActive.Should().BeFalse();
            student.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
        [Fact]
        public void Activate_WhenCalled_ShouldIsActiveTrueAndAssingUpdatedAt()
        {
            var student = CreateValidStudent();
            student.Activate();
            student.IsActive.Should().BeTrue();
            student.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
        //<----------For UpdateProfile---------->
        [Theory]
        [InlineData("Mehmet")]
        [InlineData("Cahit")]
        public void UpdateProfile_WhenValidName_ShouldUpdateName(string newName)
        {
            var student = CreateValidStudent();
            student.UpdateProfile(newName, null, null);
            student.Name.Should().Be(newName);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Cen giz")]
        [InlineData("  deniz")]
        [InlineData("12345")]
        [InlineData("Ahmet@")]
        public void UpdateProfile_WhenInvalidName_ShouldThrowException(string newName)
        {
            var student = CreateValidStudent();
            var act = () => student.UpdateProfile(newName, null, null);
            act.Should().Throw<ArgumentException>();
        }
        [Theory]
        [InlineData("Tur")]
        [InlineData("Arif")]
        public void UpdateProfile_WhenValidSurname_ShouldUpdateSurname(string newSurname)
        {
            var student = CreateValidStudent();
            student.UpdateProfile(null, newSurname, null);
            student.Surname.Should().Be(newSurname);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("T ur")]
        [InlineData("Arif@")]
        [InlineData("deniz123")]
        [InlineData("  Saygı")]
        public void UpdateProfile_WhenInvalidSurname_ShouldThrowException(string newSurname)
        {
            //Arrange 
            var student = CreateValidStudent();
            //Act
            var act = () => student.UpdateProfile(null, newSurname, null);
            //Assert
            act.Should().Throw<ArgumentException>();
        }
        [Theory]
        [InlineData(25)]
        [InlineData(70)]
        [InlineData(129)]
        public void UpdateProfile_WhenValidAge_ShouldUpdateAge(int newAge)
        {
            var student = CreateValidStudent();
            student.UpdateProfile(null, null, newAge);
            student.Age.Should().Be(newAge);
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(155)]
        [InlineData(3.8)]
        public void UpdateProfile_WhenInvalidAge_ShouldThrowException(int newAge)
        {
            var student = CreateValidStudent();
            var act = () => student.UpdateProfile(null, null, newAge);
            act.Should().Throw<ArgumentException>();
        }
        //<----------Email---------->
        [Theory]
        [InlineData("123Ahmet_Demir@test.com")]
        [InlineData("Ahmet123@test.com")]
        public void ChangeEmail_WhenValidEmail_ShouldUpdateEmail(string newEmail)
        {
            var student = CreateValidStudent();
            student.ChangeEmail(newEmail);
            student.Email.Should().Be(newEmail);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("AhmetDeniz.com")]
        [InlineData("Ahmet deniz@test.com")]
        [InlineData("AhmetDeniz@com")]
        public void ChangeEmail_WhenInvalidEmail_ShouldThrowException(string newEmail)
        {
            var student = CreateValidStudent();
            var act = () => student.ChangeEmail(newEmail);
            act.Should().Throw<ArgumentException>();
        }
        //<----------Password---------->
        [Theory]
        [InlineData("Hash_password_exemple_123")]
        [InlineData("123_exemple_password_hash")]
        public void ChangePasswordHash_WhenValidPasswordHash_ShouldUpdatePasswordHash(string passwordHash)
        {
            var student = CreateValidStudent();
            student.ChangePasswordHash(passwordHash);
            student.PasswordHash.Should().Be(passwordHash);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void ChangePasswordHash_WhenInvalidPasswordHash_ShouldThrowException(string newPasswordHassh)
        {
            var student = CreateValidStudent();
            var act = () => student.ChangePasswordHash(newPasswordHassh);
            act.Should().Throw<ArgumentException>();
        }
        //<----------SchoolNumber---------->
        [Theory]
        [InlineData("1234")]
        [InlineData("abcdas")]
        [InlineData("AHBCHS")]
        [InlineData("Ab@_?czw")]
        public void ChangeSchoolNumber_WhenValidSchoolNumber_ShouldUpdateSchoolNumber(string newSchoolNumber)
        {
            var student = CreateValidStudent();
            student.ChangeSchoolNumber(newSchoolNumber);
            student.SchoolNumber.Should().Be(newSchoolNumber);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("sdvse vefa")]
        public void ChangeSchoolNumber_WhenInvalidSchoolNumber_ShouldThrowException(string newSchoolNumber)
        {
            var student = CreateValidStudent();
            var act = () => student.ChangeSchoolNumber(newSchoolNumber);
            act.Should().Throw<ArgumentException>();
        }
        //<----------Role---------->
        [Theory]
        [InlineData(Role.Student)]
        [InlineData(Role.Teacher)]
        [InlineData(Role.Admin)]
        public void ChangeRole_WhenValidRole_ShouldUpdateRole(Role newRole)
        {
            var student = CreateValidStudent();
            student.ChangeRole(newRole);
            student.Role.Should().Be(newRole);
        }
        [Theory]
        [InlineData(-2)]
        [InlineData(33)]
        [InlineData(999)]
        public void ChangeRole_WhenInvalidRole_ShouldThrowException(int newRole)
        {
            var student = CreateValidStudent();
            var act = () => student.ChangeRole((Role)newRole);
            act.Should().Throw<ArgumentException>();
        }
        //<----------TotalCredit---------->
        [Theory]
        [InlineData(11)]
        [InlineData(49)]
        public void ChangeTotalCredit_WhenValidTotalCredit_ShouldUpdateTotalCredit(int newTotalCredit)
        {
            var student = CreateValidStudent();
            student.ChangeTotalCredit(newTotalCredit);
            student.TotalCredit.Should().Be(newTotalCredit);
        }
        [Theory]
        [InlineData(-12)]
        [InlineData(9)]
        [InlineData(51)]
        public void ChangeTotalCredit_WhenInvalidTotalCredit_ShouldThrowException(int newTotalCredit)
        {
            var student = CreateValidStudent();
            var act = () => student.ChangeTotalCredit(newTotalCredit);
            act.Should().Throw<ArgumentException>();
        }
    }
}
