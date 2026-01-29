using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.Extensions.Options;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Accounts.Infrastructure.Auth.Jwt;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Tests.Auth;

public sealed class JwtTokenServiceTests
{
    [Fact]
    public void GenerateAccessToken_Should_CreateValidJwt()
    {
        // Arrange
        IOptions<JwtOptions> options = Options.Create(
            new JwtOptions
            {
                Issuer = "test-issuer",
                Audience = "test-audience",
                SecretKey = "super_super_long_secret_key_123456789",
                AccessTokenLifetimeMinutes = 5,
                RefreshTokenLifetimeDays = 30,
            });
        var service = new JwtTokenService(options);

        // Act
        AccessToken token = service.GenerateAccessToken(
            new(
                UserId.From(Guid.NewGuid()),
                "test@email.com",
                false, true),
            new[] { "User" });

        // Assert
        token.Should().NotBeNull();
    }

    [Fact]
    public void GenerateAccessToken_Should_ContainUserIdAndEmailAndRoles()
    {
        // Arrange
        var userId = UserId.From(Guid.NewGuid());
        IOptions<JwtOptions> options = Options.Create(
            new JwtOptions
            {
                Issuer = "test-issuer",
                Audience = "test-audience",
                SecretKey = "super_super_long_secret_key_123456789",
                AccessTokenLifetimeMinutes = 5,
                RefreshTokenLifetimeDays = 30,
            });
        var service = new JwtTokenService(options);
        AccessToken token = service.GenerateAccessToken(
            new(
                userId,
                "test@email.com",
                false, true),
            new[] { "Admin", "User" });

        // Act
        JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(token.RawToken);

        // Assert
        jwt.Subject.Should().Be(userId.Value.ToString());
        jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "User");
        jwt.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Email);
    }

    [Fact]
    public void GenerateAccessToken_Should_SetExpiration()
    {
        // Arrange
        DateTime now = DateTime.UtcNow;
        IOptions<JwtOptions> options = Options.Create(
            new JwtOptions
            {
                Issuer = "test-issuer",
                Audience = "test-audience",
                SecretKey = "super_super_long_secret_key_123456789",
                AccessTokenLifetimeMinutes = 5,
                RefreshTokenLifetimeDays = 30,
            });
        var service = new JwtTokenService(options);
        AccessToken token = service.GenerateAccessToken(
            new(
                UserId.From(Guid.NewGuid()),
                "test@email.com",
                false, true),
            Array.Empty<string>());

        // Act
        JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(token.RawToken);

        // Assert
        jwt.ValidTo.Should().BeAfter(now);
    }

    [Fact]
    public void GenerateRefreshToken_Should_ReturnValidToken()
    {
        // Arrange
        IOptions<JwtOptions> options = Options.Create(
            new JwtOptions
            {
                Issuer = "test-issuer",
                Audience = "test-audience",
                SecretKey = "super_super_long_secret_key_123456789",
                AccessTokenLifetimeMinutes = 5,
                RefreshTokenLifetimeDays = 30,
            });
        var service = new JwtTokenService(options);
        var userId = UserId.New();

        // Act
        RefreshTokenEnvelope token1 = service.GenerateRefreshToken(userId);
        RefreshTokenEnvelope token2 = service.GenerateRefreshToken(userId);

        // Assert
        token1.RawToken.Should().NotBeNullOrWhiteSpace();
        token1.RawToken.Length.Should().Be(64);
        token1.ExpiresAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(30), TimeSpan.FromSeconds(1));

        // Tokens should be unique
        token1.RawToken.Should().NotBe(token2.RawToken);
    }

}
