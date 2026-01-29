using FluentValidation.TestHelper;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithPassword;

namespace SpotifyClone.Accounts.Application.Tests.Features.Auth.Commands.LoginWithPassword;

public sealed class LoginWithPasswordValidatorTests
{
    [Fact]
    public void Validate_Should_NotHaveAnyValidationErrors_When_DataIsValid()
    {
        // Arrange
        var validator = new LoginWithPasswordCommandValidator();

        // Act
        TestValidationResult<LoginWithPasswordCommand> result = validator.TestValidate(
            new LoginWithPasswordCommand("example@gmail.com", "StrongPass123"));

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Email_Should_NotBeNullOrEmpty(string? invalidEmail)
    {
        // Arrange
        var validator = new LoginWithPasswordCommandValidator();

        // Act
        TestValidationResult<LoginWithPasswordCommand> result = validator.TestValidate(
            new LoginWithPasswordCommand(invalidEmail!, "StrongPass123!"));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Password_Should_NotBeNullOrEmpty(string? invalidPassword)
    {
        // Arrange
        var validator = new LoginWithPasswordCommandValidator();

        // Act
        TestValidationResult<LoginWithPasswordCommand> result = validator.TestValidate(
            new LoginWithPasswordCommand("example@gmail.com", invalidPassword!));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}
