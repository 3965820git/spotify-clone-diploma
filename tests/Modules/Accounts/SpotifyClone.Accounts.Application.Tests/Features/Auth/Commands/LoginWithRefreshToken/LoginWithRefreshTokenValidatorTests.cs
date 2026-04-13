using FluentValidation.TestHelper;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.RefreshToken;

namespace SpotifyClone.Accounts.Application.Tests.Features.Auth.Commands.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenValidatorTests
{
    [Fact]
    public void Validate_Should_NotHaveAnyValidationErrors_When_DataIsValid()
    {
        // Arrange
        var validator = new LoginUserWithRefreshTokenCommandValidator();

        // Act
        TestValidationResult<LoginUserWithRefreshTokenCommand> result = validator.TestValidate(
            new LoginUserWithRefreshTokenCommand("rawRefreshToken"));

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Email_Should_NotBeNullOrEmpty(string? invalidRefreshToken)
    {
        // Arrange
        var validator = new LoginUserWithRefreshTokenCommandValidator();

        // Act
        TestValidationResult<LoginUserWithRefreshTokenCommand> result = validator.TestValidate(
            new LoginUserWithRefreshTokenCommand(invalidRefreshToken!));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.RawToken);
    }
}
