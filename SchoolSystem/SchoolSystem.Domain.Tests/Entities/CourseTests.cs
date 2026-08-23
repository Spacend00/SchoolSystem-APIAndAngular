
using FluentAssertions;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Domain.Tests.Entities
{
    public class CourseTests
    {
        private Course CreateValidCourse(string name = "Matematik", int credit = 5)
        {
            return new Course(name, credit);
        }
        //<---------Constructor--------->
        [Fact]
        public void Constructor_WhenAllDataIsValid_ShouldCreateCourse()
        {
            var course = CreateValidCourse();

            course.Id.Should().NotBe(Guid.Empty);
            course.Name.Should().Be("Matematik");
            course.Credit.Should().Be(5);
            course.IsActive.Should().BeTrue();
            course.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            course.UpdatedAt.Should().Be(null);
        }
        [Fact]
        public void Constructor_WhenNameIsEmpty_ShouldThrowException()
        {
            var act = () => new Course("", 3);
            act.Should().Throw<ArgumentException>();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public void Constructor_WhenCreditIsOutOfRange_ShouldThrowException(int newCredit) 
        {
            var act = () => new Course("Fizik", newCredit);
            act.Should().Throw<ArgumentException>();
        }
        //<---------For IsActive--------->
        [Fact]
        public void Deactivate_WhenCalled_ShouldIsActiveFalseAndAssingUpdatedAt()
        {
            var course = CreateValidCourse();
            course.Deactivate();
            course.IsActive.Should().BeFalse();
            course.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
        [Fact]
        public void Activate_WhenCalled_ShouldIsActiveTrueAndAssingUpdatedAt()
        {
            var course = CreateValidCourse();
            course.Activate();
            course.IsActive.Should().BeTrue();
            course.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
        //<---------UpdateCourse--------->
        [Theory]
        [InlineData("Biyoloji")]
        [InlineData("Türkçe")]
        public void UpdateCourse_WhenValidName_ShouldUpdateName(string newName)
        {
            var course = CreateValidCourse();
            course.UpdateCourse(newName, null);
            course.Name.Should().Be(newName);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("ders deneme")]
        [InlineData("ders@")]
        [InlineData("123_ders.com")]
        public void UpdateCourse_WhenInvalidName_ShouldThrowException(string newName)
        {
            var course = CreateValidCourse();
            var act = () => course.UpdateCourse(newName, null);
            act.Should().Throw<ArgumentException>();            
        }
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void UpdateCourse_WhenValidCredit_ShouldUpdateCredit(int newcredit)
        {
            var course = CreateValidCourse();
            course.UpdateCourse(null, newcredit);
            course.Credit.Should().Be(newcredit);
        }
        [Theory]
        [InlineData(-3)]
        [InlineData(23)]
        [InlineData(99)]
        public void UpdateCourse_WhenInvalidCredit_ShouldThrowException(int newCredit) 
        {
            var course = CreateValidCourse();
            var act = () => course.UpdateCourse(null, newCredit);
            act.Should().Throw<ArgumentException>();
        }
    }
}
