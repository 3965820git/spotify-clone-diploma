using FluentValidation.TestHelper;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithRefreshToken;

namespace SpotifyClone.Accounts.Application.Tests.Features.Auth.Commands.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenValidatorTests
{
    [Fact]
    public void Validate_Should_NotHaveAnyValidationErrors_When_DataIsValid()
    {
        // Arrange
        var validator = new LoginWithRefreshTokenCommandValidator();

        // Act
        TestValidationResult<LoginWithRefreshTokenCommand> result = validator.TestValidate(
            new LoginWithRefreshTokenCommand("rawRefreshToken"));

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Email_Should_NotBeNullOrEmpty(string? invalidRefreshToken)
    {
        // Arrange
        var validator = new LoginWithRefreshTokenCommandValidator();

        // Act
        TestValidationResult<LoginWithRefreshTokenCommand> result = validator.TestValidate(
            new LoginWithRefreshTokenCommand(invalidRefreshToken!));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.RawToken);
    }
}
