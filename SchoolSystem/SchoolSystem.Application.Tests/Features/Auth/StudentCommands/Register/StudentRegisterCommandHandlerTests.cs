
using FluentAssertions;
using Moq;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Application.Features.Auth.StudentCommands.Register;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Application.Tests.Features.Auth.StudentCommands.Register
{
    public class StudentRegisterCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly StudentRegisterCommandHandler _handler;
        public StudentRegisterCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _hasherMock = new Mock<IPasswordHasher>();
            _handler = new StudentRegisterCommandHandler(_repositoryMock.Object, _hasherMock.Object);
        }
        private StudentRegisterCommand CreateValidCommand()
        {
            return new StudentRegisterCommand { Name = "Ahmet", Surname = "Deniz", Age = 33, SchoolNumber = "sc-2345", Email = "Test@Test.com", Password = "Test1234" };
        }
        [Fact]
        public async Task Handler_WhenDataIsValid_ShouldCreateStudent()
        {
            //Arrange
            var command = CreateValidCommand();
            _repositoryMock.Setup(r => r.ExistsByEmailAsync(command.Email)).ReturnsAsync(false);
            _repositoryMock.Setup(r => r.ExistsBySchoolNumberAsync(command.SchoolNumber)).ReturnsAsync(false);
            _hasherMock.Setup(h => h.HashPassword(command.Password)).Returns("hashed_password_123");
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(command.Email);
            result.Id.Should().NotBe(Guid.Empty);
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Student>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task Handler_WhenEmailAlredyExist_ShouldThrowException() 
        {
            //Arrange
            var command = CreateValidCommand();
            _repositoryMock.Setup(r => r.ExistsByEmailAsync(command.Email)).ReturnsAsync(true);
            //Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);
            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Student>()), Times.Never());
        }
        [Fact]
        public async Task Handler_WhenSchoolNumberAlreadyExist_ShouldThrowException()
        {
            //Arrange
            var command = CreateValidCommand();
            _repositoryMock.Setup(r => r.ExistsBySchoolNumberAsync(command.SchoolNumber)).ReturnsAsync(true);
            //Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);
            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Student>()), Times.Never());
        }
    }
}
