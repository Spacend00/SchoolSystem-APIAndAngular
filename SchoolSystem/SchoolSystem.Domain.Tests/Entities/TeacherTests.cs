
using FluentAssertions;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Domain.Tests.Entities
{
    public class TeacherTests
    {
        private Teacher CreateValidTeacher(
            string name = "Hasan",
            string surname = "Yılmaz",
            int age = 35,
            string email = "Hasanyılmaz@test.com",
            string passwordHash = "hashed_password_123",
            Branch branch = Branch.Biyoloji
            )
        {
            return new Teacher(name, surname, age, email, passwordHash, branch);
        }
        //<-----------For Constructor----------->
        [Fact]
        public void Constructor_ShouldCreateStudent_WhenAllDataIsValid()
        {
            var teacher = CreateValidTeacher();

            teacher.Id.Should().NotBe(Guid.Empty);
            teacher.Name.Should().Be("Hasan");
            teacher.Surname.Should().Be("Yılmaz");
            teacher.Age.Should().Be(35);
            teacher.Email.Should().Be("Hasanyılmaz@test.com");
            teacher.PasswordHash.Should().Be("hashed_password_123");
            teacher.IsActive.Should().BeTrue();
            teacher.Branch.Should().Be(Branch.Biyoloji);
            teacher.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            teacher.UpdatedAt.Should().Be(null);
        }
        [Fact]
        public void Constructor_WhenNameIsEmpty_ShouldThrowException()
        {
            var act = () => new Teacher("", "Yılmaz", 35, "Hasanyılmaz@test.com", "hashed_password_123", Branch.Biyoloji);
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_WhenSurnameIsEmpty_ShouldThrowException()
        {
            var act = () => new Teacher("Hasan", "", 35, "Hasanyılmaz@test.com", "hashed_password_123", Branch.Biyoloji);
            act.Should().Throw<ArgumentException>();
        }
        [Theory]
        [InlineData(20)]
        [InlineData(130)]
        public void Constructor_WhenAgeOutOfRange_ShouldThrowException(int age)
        {
            var act = () => new Teacher("Hasan", "Yılmaz", age, "Hasanyılmaz@test.com", "hashed_password_123", Branch.Biyoloji);
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_WhenEmailIsEmpty_ShouldThrowException()
        {
            var act = () => new Teacher("Hasan", "Yılmaz", 35, "", "hashed_password_123", Branch.Biyoloji);
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_WhenPassworHashIsEmpty_ShouldThrowException()
        {
            var act = () => new Teacher("Hasan", "Yılmaz", 35, "Hasanyılmaz@test.com", "", Branch.Biyoloji);
            act.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Constructor_WhenBranchIsEmpty_ShouldThrowException()
        {
            var act = () => new Teacher("Hasan", "Yılmaz", 35, "Hasanyılmaz@test.com", "hashed_password_123", (Branch)999);
            act.Should().Throw<ArgumentException>();
        }
        [Theory]
        [InlineData(-22)]
        [InlineData(55)]
        public void Constructor_WhenInvalidBranch_ShouldThrowException(int branchValue)
        {
            var act = () => new Teacher("Hasan", "Yılmaz", 35, "Hasanyılmaz@test.com", "hashed_password_123", (Branch)branchValue);
            act.Should().Throw<ArgumentException>();
        }
        //<-----------For IsActivate----------->
        [Fact]
        public void Deactivate_WhenCall_ShouldIsActivateFalseAndAssingUpdatedAt()
        {
            var teacher = CreateValidTeacher();
            teacher.Deactivate();
            teacher.IsActive.Should().BeFalse();
            teacher.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
        [Fact]
        public void Deactivate_WhenCall_ShouldIsActivateTrueAndAssingUpdatedAt()
        {
            var teacher = CreateValidTeacher();
            teacher.Activate();
            teacher.IsActive.Should().BeTrue();
            teacher.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
        //<-----------For UpdateProfile----------->
        [Theory]
        [InlineData("mirac")]
        [InlineData("MEHMET")]
        public void UpdateProfile_WhenValidName_ShouldUpdateName(string newName)
        {
            var teacher = CreateValidTeacher();
            teacher.UpdateProfile(newName, null, null);
            teacher.Name.Should().Be(newName);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123")]
        [InlineData("reyhan123")]
        [InlineData("rey-han@")]
        public void UpdateProfile_WhenInvalidName_ShouldThrowException(string newName)
        {
            var teacher = CreateValidTeacher();
            var act = () => teacher.UpdateProfile(newName, null, null);
            act.Should().Throw<ArgumentException>();            
        }
        [Theory]
        [InlineData("Diyar")]
        [InlineData("Cihan")]
        public void UpdateProfile_WhenValidSurname_ShouldUpdateSurname(string newSurname)
        {
            var teacher = CreateValidTeacher();
            teacher.UpdateProfile(null, newSurname, null);
            teacher.Surname.Should().Be(newSurname);
        }
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("123")]
        [InlineData("yılmaz123")]
        [InlineData("yıl_maz@")]
        public void UpdateProfile_WhenInvalidSurname_ShouldThrowException(string nevSurname)
        {
            var teacher = CreateValidTeacher();
            var act = () => teacher.UpdateProfile(null, nevSurname, null);
            act.Should().Throw<ArgumentException>();
        }
        [Theory]
        [InlineData(21)]
        [InlineData(129)]
        public void UpdateProfile_WhenValidAge_ShouldUpdateAge(int newAge)
        {
            var teacher = CreateValidTeacher();
            teacher.UpdateProfile(null, null, newAge);
            teacher.Age.Should().Be(newAge);
        }
        [Theory]
        [InlineData(-10)]
        [InlineData(5)]
        [InlineData(142)]
        public void UpdateProfile_WhenInvalidAge_ShouldThrowException(int newAge)
        {
            var teacher = CreateValidTeacher();
            var act = () => teacher.UpdateProfile(null, null, newAge);
            act.Should().Throw<ArgumentException>();
        }
        //<-----------ChangeEmail----------->
        [Theory]
        [InlineData("AhmetYılmaz@test.com")]
        [InlineData("Ahmet_yilmaz123@test.com")]
        public void ChangeEmail_WhenValidEmail_ShouldUpdateEmail(string email)
        {
            var teacher = CreateValidTeacher();
            teacher.ChangeEmail(email);
            teacher.Email.Should().Be(email);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("ahmet yılmaz@test.com")]
        [InlineData("ahmet.com")]
        [InlineData("Yılmaz@")]
        [InlineData("13Ahmet")]
        public void ChangeEmail_WhenInvalidEmail_ShouldThrowException(string email)
        {
            var teacher = CreateValidTeacher();
            var act = () => teacher.ChangeEmail(email);
            act.Should().Throw<ArgumentException>();
        }
        //<-----------ChangePasswordHash----------->
        [Theory]
        [InlineData("Hash_password_123")]
        [InlineData("123_hash_password")]
        public void ChangeEmail_WhenValidPasswordHash_ShouldUpdatePasswordHash(string passwordHash)
        {
            var teacher = CreateValidTeacher();
            teacher.ChangePasswordHash(passwordHash);
            teacher.PasswordHash.Should().Be(passwordHash);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void ChangeEmail_WhenInvalidPasswordHash_ShouldThrowException(string passwordHash)
        {
            var teacher = CreateValidTeacher();
            var act = () => teacher.ChangePasswordHash(passwordHash);
            act.Should().Throw<ArgumentException>();
        }
        //<-----------ChangeRole----------->
        [Theory]
        [InlineData(Role.Student)]
        [InlineData(Role.Teacher)]
        [InlineData(Role.Admin)]
        public void ChangeEmail_WhenValidRole_ShouldUpdateRole(Role role)
        {
            var teacher = CreateValidTeacher();
            teacher.ChangeRole(role);
            teacher.Role.Should().Be(role);
        }
        [Theory]
        [InlineData(-11)]
        [InlineData(5)]
        [InlineData(99)]
        public void ChangeEmail_WhenInvalidRole_ShouldThrowException(int roleValue)
        {
            var teacher = CreateValidTeacher();
            var act = () => teacher.ChangeRole((Role)roleValue);
            act.Should().Throw<ArgumentException>();
        }
        //<-----------ChangeBranch----------->
        [Theory]
        [InlineData(Branch.BilişimTeknolojileri)]
        [InlineData(Branch.Matematik)]
        public void ChangeEmail_WhenValidBranch_ShouldUpdateBranch(Branch branch)
        {
            var teacher = CreateValidTeacher();
            teacher.ChangeBranch(branch);
            teacher.Branch.Should().Be(branch);
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(35)]
        [InlineData(999)]
        public void ChangeEmail_WhenInvalidBranch_ShouldThrowException(int branchValue)
        {
            var teacher = CreateValidTeacher();
            var act = () => teacher.ChangeBranch((Branch)branchValue);
            act.Should().Throw<ArgumentException>();
        }

    }
}
