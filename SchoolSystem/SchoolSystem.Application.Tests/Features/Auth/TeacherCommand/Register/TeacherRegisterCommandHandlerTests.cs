
using FluentAssertions;
using Moq;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Application.Features.Auth.TeacherCommands.Register;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Domain.Enums;

namespace SchoolSystem.Application.Tests.Features.Auth.TeacherCommand.Register
{
    public class TeacherRegisterCommandHandlerTests
    {
        private readonly Mock<ITeacherRepository> _repositoryMock;
        private readonly Mock<IPasswordHasher> _hasherMoch;
        private readonly TeacherRegisterCommandHandler _handler;
        public TeacherRegisterCommandHandlerTests() 
        {
            _repositoryMock = new Mock<ITeacherRepository>();
            _hasherMoch = new Mock<IPasswordHasher>();
            _handler = new TeacherRegisterCommandHandler(_repositoryMock.Object, _hasherMoch.Object);
        }
        private TeacherRegisterCommand CreateValidCommand() 
        {
            return new TeacherRegisterCommand { Name = "Salih", Surname = "Demir", Age = 45, Email = "Salih_123@test.com", Password = "Salih_123", Branch = Branch.Arapça };
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmailIsExists()
        {
            var command = CreateValidCommand();            
            _repositoryMock.Setup(r => r.ExistsByEmailAsync(command.Email)).ReturnsAsync(true);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InvalidOperationException>();

            _hasherMoch.Verify(p => p.HashPassword(It.IsAny<string>()), Times.Never());
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Teacher>()), Times.Never());
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());
        }
        [Fact]
        public async Task Handle_ShouldCreateTeacherAndReturnResponse_WhenCommandIsValid()
        {
            var command = CreateValidCommand();
            string hashedPassword = "hashed_password_123";
            _repositoryMock.Setup(r => r.ExistsByEmailAsync(command.Email)).ReturnsAsync(false);
            _hasherMoch.Setup(r => r.HashPassword(command.Password)).Returns(hashedPassword);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Email.Should().Be(command.Email);
            result.Id.Should().NotBe(Guid.Empty);

            _repositoryMock.Verify(r => r.CreateAsync(It.Is<Teacher>(t => t.Email == command.Email && t.PasswordHash == hashedPassword)), Times.Once());
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once());
        }
    }
}
