using FluentAssertions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Application.Features.Auth.TeacherCommands.Login;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Domain.Enums;
using SchoolSystem.Domain.Interfaces;

namespace SchoolSystem.Application.Tests.Features.Auth.TeacherCommand.Login
{
    public class TeacherLoginCommandHandlerTests
    {
        private readonly Mock<ITeacherRepository> _repositoryMock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly Mock<ITokenService<IUserEntity>> _tokenMock;
        private readonly TeacherLoginCommandHandler _handler;
        public TeacherLoginCommandHandlerTests()
        {
            _repositoryMock = new Mock<ITeacherRepository>();
            _hasherMock = new Mock<IPasswordHasher>();
            _tokenMock = new Mock<ITokenService<IUserEntity>>();
            _handler = new TeacherLoginCommandHandler(_repositoryMock.Object, _hasherMock.Object, _tokenMock.Object);
        }
        private Teacher CreateValidTeacher()
        {
            return new Teacher("Salih", "Demir", 45, "Salih_123@test.com", "hashed_password_123", Branch.Arapça);
        }
        private TeacherLoginCommand CreateValidCommand()
        {
            return new TeacherLoginCommand { Email = "Salih_123@test.com", Password = "Salih_123" };
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmailIsNotTrue()
        {
            var command = CreateValidCommand();
            _repositoryMock.Setup(r => r.GetByEmailAsync(command.Email)).ReturnsAsync((Teacher?)null);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _hasherMock.Verify(p => p.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            _tokenMock.Verify(t => t.GenerateToken(It.IsAny<IUserEntity>()), Times.Never());
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenPasswordIsIncorrect()
        {
            var command = CreateValidCommand();
            var teacher = CreateValidTeacher();
            _repositoryMock.Setup(r => r.GetByEmailAsync(command.Email)).ReturnsAsync(teacher);
            _hasherMock.Setup(p => p.VerifyPassword(command.Password, teacher.PasswordHash)).Returns(false);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _repositoryMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Once());
            _hasherMock.Verify(p => p.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _tokenMock.Verify(t => t.GenerateToken(It.IsAny<IUserEntity>()), Times.Never());
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenTeacherIsNotActive()
        {
            var command = CreateValidCommand();
            var teacher = CreateValidTeacher();
            teacher.Deactivate();
            _repositoryMock.Setup(r => r.GetByEmailAsync(command.Email)).ReturnsAsync(teacher);
            _hasherMock.Setup(p => p.VerifyPassword(command.Password, teacher.PasswordHash)).Returns(true);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
            _repositoryMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Once());
            _hasherMock.Verify(p => p.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _tokenMock.Verify(t => t.GenerateToken(It.IsAny<IUserEntity>()), Times.Never());
        }
        [Fact]
        public async Task Handle_ShouldReturnResponse_WhenTeacherIsValid()
        {
            var command = CreateValidCommand();
            var teacher = CreateValidTeacher();
            string token = "token_123";
            _repositoryMock.Setup(r => r.GetByEmailAsync(command.Email)).ReturnsAsync(teacher);
            _hasherMock.Setup(p => p.VerifyPassword(command.Password, teacher.PasswordHash)).Returns(true);
            _tokenMock.Setup(t => t.GenerateToken(teacher)).ReturnsAsync(token);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Token.Should().Be(token);
            _repositoryMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Once());
            _hasherMock.Verify(p => p.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _tokenMock.Verify(t => t.GenerateToken(It.IsAny<IUserEntity>()), Times.Once());
        }

    }
}
