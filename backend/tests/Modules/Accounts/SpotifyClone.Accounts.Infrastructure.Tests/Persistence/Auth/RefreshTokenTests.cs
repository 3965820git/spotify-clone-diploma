using FluentAssertions;
using SpotifyClone.Accounts.Infrastructure.Persistence.Auth;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Tests.Persistence.Auth;

public sealed class RefreshTokenTests
{
    [Fact]
    public void Constructor_Should_AssignValues_When_ParametersAreValid()
    {
        // Arrange
        var userId = UserId.New();
        string tokenHash = "tokenHash";
        DateTimeOffset expiresAt = DateTimeOffset.MaxValue;

        // Act
        var refreshToken = new RefreshToken(userId, tokenHash, expiresAt);

        // Assert
        refreshToken.UserId.Should().Be(userId);
        refreshToken.TokenHash.Should().Be(tokenHash);
        refreshToken.ExpiresAt.Should().Be(expiresAt);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_Should_ThrowException_When_TokenHashIsNullOrWhitespace(string? invalidTokenHash)
    {
        // Arrange
        var userId = UserId.New();
        DateTimeOffset expiresAt = DateTimeOffset.MaxValue;

        // Act
        Func<RefreshToken> result = () => new RefreshToken(userId, invalidTokenHash!, expiresAt);

        // Arrange
        result.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Revoke_Should_AssignRevokedAt()
    {
        // Arrange
        var userId = UserId.New();
        string tokenHash = "tokenHash";
        DateTimeOffset expiresAt = DateTimeOffset.MaxValue;
        var refreshToken = new RefreshToken(userId, tokenHash, expiresAt);

        // Act
        refreshToken.RevokedAt.Should().BeNull();
        refreshToken.Revoke();

        // Assert
        refreshToken.RevokedAt.Should().NotBeNull();
    }

    [Fact]
    public void Revoke_Should_AssignReplacedByTokenHash_When_ParameterIsValid()
    {
        // Arrange
        var userId = UserId.New();
        string tokenHash = "tokenHash";
        DateTimeOffset expiresAt = DateTimeOffset.MaxValue;
        var refreshToken = new RefreshToken(userId, tokenHash, expiresAt);
        string otherTokenHash = "otherTokenHash";

        // Act
        refreshToken.RevokedAt.Should().BeNull();
        refreshToken.Revoke(otherTokenHash);

        // Assert
        refreshToken.RevokedAt.Should().NotBeNull();
        refreshToken.ReplacedByTokenHash.Should().Be(otherTokenHash);
    }
}
