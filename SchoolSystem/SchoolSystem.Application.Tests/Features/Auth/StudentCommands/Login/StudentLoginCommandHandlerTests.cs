
using FluentAssertions;
using Moq;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Application.Features.Auth.StudentCommands.Login;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Application.Tests.Features.Auth.StudentCommands.Login
{
    public class StudentLoginCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHassherMock;
        private readonly Mock<ITokenService<IUserEntity>> _tokenServiceMock;
        private readonly StudentLoginCommandHandler _handler;
        public StudentLoginCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _passwordHassherMock = new Mock<IPasswordHasher>();
            _tokenServiceMock = new Mock<ITokenService<IUserEntity>>();
            _handler = new StudentLoginCommandHandler(_repositoryMock.Object, _passwordHassherMock.Object, _tokenServiceMock.Object);
        }
        private StudentLoginCommand CreateValidCommand()
        {
            return new StudentLoginCommand { Email = "Student@test.com", Password = "Password123!" };
        }
        private Student CreateValidStudent()
        {
            return new Student("Ahmet", "Deniz", 33, "sc-2345", "Student@test.com", "Hashed_password_123");
        }
        [Fact]
        public async Task Handle_WhenStudentNotFound_ShouldThrowException()
        {
            var command = CreateValidCommand();
            _repositoryMock.Setup(r => r.GetByEmailAsync(command.Email)).ReturnsAsync((Student?)null);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _passwordHassherMock.Verify(p => p.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            _tokenServiceMock.Verify(t => t.GenerateToken(It.IsAny<IUserEntity>()), Times.Never());
        }
        [Fact]
        public async Task Handle_WhenPasswordIncorrect_ShouldThrowException()
        {
            var command = CreateValidCommand();
            var student = CreateValidStudent();
            _repositoryMock.Setup(c => c.GetByEmailAsync(command.Email)).ReturnsAsync(student);
            _passwordHassherMock.Setup(p => p.VerifyPassword(command.Password, student.PasswordHash)).Returns(false);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _tokenServiceMock.Verify(t => t.GenerateToken(It.IsAny<IUserEntity>()), Times.Never());
        }
        [Fact]
        public async Task Handle_WhenIsActiveFalse_ShouldThrowException()
        {
            var command = CreateValidCommand();
            var student = CreateValidStudent();
            student.Deactivate();
            _repositoryMock.Setup(r => r.GetByEmailAsync(command.Email)).ReturnsAsync(student);
            _passwordHassherMock.Setup(p => p.VerifyPassword(command.Password, student.PasswordHash)).Returns(true);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _tokenServiceMock.Verify(t => t.GenerateToken(student), Times.Never());
            
        }
        [Fact]
        public async Task Handle_WhenLoginIsSuccesful_ShouldLoginAccount()
        {
            var command = CreateValidCommand();
            var student = CreateValidStudent();
            string expectedToken = "mocked-jwt-token-xyz";
            _repositoryMock.Setup(r => r.GetByEmailAsync(command.Email)).ReturnsAsync(student);
            _passwordHassherMock.Setup(p => p.VerifyPassword(command.Password, student.PasswordHash)).Returns(true);
            _tokenServiceMock.Setup(t => t.GenerateToken(student)).ReturnsAsync(expectedToken);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Token.Should().Be(expectedToken);
            _tokenServiceMock.Verify(t => t.GenerateToken(It.IsAny<IUserEntity>()), Times.Once());
        }


    }
}
