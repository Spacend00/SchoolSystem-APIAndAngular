
using FluentAssertions;
using Moq;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Application.Features.Students.DeleteStudents;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Application.Tests.Features.Students.Delete
{
    public class SoftDeleteStudentCommandHandlerTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly SoftDeleteStudentCommandHandler _handler;
        public SoftDeleteStudentCommandHandlerTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _handler = new SoftDeleteStudentCommandHandler(_repositoryMock.Object);
        }
        private SoftDeleteStudentCommand CreateValidCommand()
        {
            return new SoftDeleteStudentCommand(Guid.NewGuid());
        }
        private Student CreateValidStudent()
        {
            return new Student("Ali", "Değirmenci", 21, "sc-455", "Degirmenci_123@Test.com", "hashed_password_123");
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenStudentIsNull()
        {
            var command = CreateValidCommand();
            _repositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Student?)null);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InvalidOperationException>();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenStudentIsActiveIsFalse()
        {
            var command = CreateValidCommand();
            var student = CreateValidStudent();
            _repositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(student);
            student.Deactivate();

            var act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());
        }
        [Fact]
        public async Task Handle_ShouldReturnResponse_WhenStudentIsValid()
        {
            var command = CreateValidCommand();
            var student = CreateValidStudent();
            _repositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(student);

            var result = _handler.Handle(command, CancellationToken.None);

            student.IsActive.Should().BeFalse();
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once());
        }
    }
}
