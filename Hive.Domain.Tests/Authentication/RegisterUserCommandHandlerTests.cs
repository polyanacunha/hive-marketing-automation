

using FluentAssertions;
using Hive.Application.Interfaces;
using Hive.Application.UseCases.Authentication.Command;
using Hive.Domain.Validation;
using Moq;
using Xunit;

namespace Hive.Domain.Tests.Authentication
{
    public class RegisterUserCommandHandlerTests
    {
        private readonly Mock<IAuthenticate> _mockAuthenticate;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserCommandHandlerTests()
        {
            _mockAuthenticate = new Mock<IAuthenticate>();
            _mockEmailService = new Mock<IEmailService>();
            _handler = new RegisterUserCommandHandler(_mockAuthenticate.Object, _mockEmailService.Object);
        }

        [Fact(DisplayName = "Should register a user successfully")]
        public async Task Handle_WhenEmailIsUnique_ShouldCallRegisterAndReturnSuccess()
        {
            var command = new RegisterUserCommand("teste@gmail.com", "Password1@");

            _mockAuthenticate
            .Setup(s => s.UserExists(command.Email))
            .ReturnsAsync(Result<bool>.Success(false));

            var expectedUserId = "guid";
            var expectedToken = "fake.jwt.token.string";
            var expectedResultTuple = (userId: expectedUserId, token: expectedToken);

            _mockAuthenticate
            .Setup(s => s.Register(command.Email, command.Password))
            .ReturnsAsync(Result<(string userId, string token)>.Success(expectedResultTuple));

            
            var expectedToEmail = command.Email;
            var expectedSubject = "Confirmacão de Email";
            var expectedBody = $"Clique no para confirmar email: https://localhost:7121?token={expectedToken}";

            _mockEmailService
            .Setup(s => s.SendEmail(expectedToEmail, expectedSubject, expectedBody))
            .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(expectedUserId);

            _mockAuthenticate.Verify(s => s.UserExists(command.Email), Times.Once);
            _mockAuthenticate.Verify(s => s.Register(command.Email, command.Password), Times.Once);
            _mockEmailService.Verify(s => s.SendEmail(expectedToEmail, expectedSubject, expectedBody), Times.Once);

        }

        [Fact(DisplayName = "Should return failure when email already exists")]
        public async Task Handle_WhenEmailIsInvalid_ShouldCallRegisterAndReturnFailure()
        {
            var command = new RegisterUserCommand("teste@invalido.com", "Password1@");

            _mockAuthenticate
            .Setup(s => s.UserExists(command.Email))
            .ReturnsAsync(Result<bool>.Success(true));

            var result =  await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo("Email already registered");

            _mockAuthenticate.Verify(s => s.UserExists(command.Email), Times.Once); 
            _mockAuthenticate.Verify(s => s.Register(command.Email, command.Password), Times.Never);
            _mockEmailService.Verify(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        }

        [Fact(DisplayName = "Should return failure when password is invalid")]
        public async Task Handle_WhenPasswordIsInvalid_ShouldCallRegisterAndReturnFailure()
        {
            var command = new RegisterUserCommand("teste@teste.com", "str");

            _mockAuthenticate
            .Setup(s => s.UserExists(command.Email))
            .ReturnsAsync(Result<bool>.Success(false));

            var expectedErrors = new[]
            {
                "Passwords must be at least 8 characters.",
                "Passwords must have at least one non alphanumeric character.",
                "Passwords must have at least one digit ('0'-'9').",
                "Passwords must have at least one uppercase ('A'-'Z')."
            };

            _mockAuthenticate
            .Setup(s => s.Register(command.Email, command.Password))
            .ReturnsAsync(Result<(string userId, string token)>.Failure(expectedErrors));

            var result = await _handler.Handle(command, CancellationToken.None);


            result.Should().NotBeNull();
            result.IsFailure.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(expectedErrors);

            _mockAuthenticate.Verify(s => s.UserExists(command.Email), Times.Once);
            _mockAuthenticate.Verify(s => s.Register(command.Email, command.Password), Times.Once);
            _mockEmailService.Verify(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

        }

    }

}