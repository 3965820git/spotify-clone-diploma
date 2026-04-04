using System.Security.Claims;
using FluentAssertions;
using Moq;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.Login;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Password;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Tests.Features.Auth.Commands.LoginWithPassword;

public sealed class LoginWithPasswordCommandHandlerTests
{
    private readonly Mock<IIdentityService> _identityMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IAccountsUnitOfWork> _unitMock = new();
    private readonly Mock<ITokenHasher> _tokenHasherMock = new();
    private readonly LoginUserWithPasswordCommandHandler _handler;

    public LoginWithPasswordCommandHandlerTests()
        => _handler = new LoginUserWithPasswordCommandHandler(
            _unitMock.Object,
            _identityMock.Object,
            _tokenServiceMock.Object,
            _tokenHasherMock.Object);

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_IdentityValidationFails()
    {
        // Arrange
        var command = new LoginUserWithPasswordCommand("test@test.com", "Password123!");
        Error error = AuthErrors.InvalidIdentifier;

        _identityMock
            .Setup(x => x.ValidateUserAsync(command.Identifier, command.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<IdentityUserInfo>(error));

        // Act
        Result<LoginUserCommandResult> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e == error);
        _identityMock.Verify(
            x => x.ValidateUserAsync(command.Identifier, command.Password, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_RevokeRefreshTokensFails()
    {
        // Arrange
        var command = new LoginUserWithPasswordCommand("test@test.com", "Password123!");
        var userId = UserId.New();
        var identityInfo = new IdentityUserInfo(userId, "test@test.com", null, true, false, false);

        _identityMock
            .Setup(x => x.ValidateUserAsync(command.Identifier, command.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(identityInfo));

        _identityMock
            .Setup(x => x.GetUserRolesAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<IReadOnlyCollection<string>>(Array.Empty<string>()));

        _identityMock
            .Setup(x => x.GetUserClaimsAsync(userId))
            .ReturnsAsync(Result.Success<IReadOnlyCollection<Claim>>(Array.Empty<Claim>()));

        _tokenServiceMock
            .Setup(x => x.GenerateAccessToken(identityInfo, It.IsAny<IReadOnlyCollection<string>>()))
            .Returns(new AccessToken("accessToken", DateTimeOffset.UtcNow.AddMinutes(5)));

        _tokenServiceMock
            .Setup(x => x.GenerateRefreshToken(userId))
            .Returns(new RefreshTokenEnvelope(userId, "rawRefreshToken", DateTimeOffset.UtcNow.AddDays(30), true));

        _tokenHasherMock
            .Setup(x => x.Hash("rawRefreshToken"))
            .Returns("hashedRefreshToken");

        _unitMock
            .Setup(x => x.RefreshTokens.RevokeAllAsync(
                userId,
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure());

        // Act
        Result<LoginUserCommandResult> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();

        _unitMock.Verify(
            x => x.RefreshTokens.RevokeAllAsync(
                userId,
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_RefreshTokenStoreFails()
    {
        // Arrange
        var command = new LoginUserWithPasswordCommand("test@test.com", "Password123!");
        var userId = UserId.New();
        var identityInfo = new IdentityUserInfo(userId, "test@test.com", null, true, false, false);

        _identityMock
            .Setup(x => x.ValidateUserAsync(command.Identifier, command.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(identityInfo));

        _identityMock
            .Setup(x => x.GetUserRolesAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<IReadOnlyCollection<string>>(Array.Empty<string>()));

        _identityMock
            .Setup(x => x.GetUserClaimsAsync(userId))
            .ReturnsAsync(Result.Success<IReadOnlyCollection<Claim>>(Array.Empty<Claim>()));

        _tokenServiceMock
            .Setup(x => x.GenerateAccessToken(identityInfo, It.IsAny<IReadOnlyCollection<string>>()))
            .Returns(new AccessToken("accessToken", DateTimeOffset.UtcNow.AddMinutes(5)));

        _tokenServiceMock
            .Setup(x => x.GenerateRefreshToken(userId))
            .Returns(new RefreshTokenEnvelope(userId, "rawRefreshToken", DateTimeOffset.UtcNow.AddDays(30), true));

        _tokenHasherMock
            .Setup(x => x.Hash("rawRefreshToken"))
            .Returns("hashedRefreshToken");

        Error error = RefreshTokenErrors.InvalidToken;

        _unitMock
            .Setup(x => x.RefreshTokens.RevokeAllAsync(
                userId,
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _unitMock
            .Setup(x => x.RefreshTokens.StoreAsync(
                userId,
                "hashedRefreshToken",
                It.IsAny<DateTimeOffset>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(error));

        // Act
        Result<LoginUserCommandResult> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e == error);

        _unitMock.Verify(
            x => x.RefreshTokens.RevokeAllAsync(
                userId,
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitMock.Verify(
            x => x.RefreshTokens.StoreAsync(
                userId, "hashedRefreshToken",
                It.IsAny<DateTimeOffset>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_LoginSucceeds()
    {
        // Arrange
        var command = new LoginUserWithPasswordCommand("test@test.com", "Password123!");
        var userId = UserId.New();
        var identityInfo = new IdentityUserInfo(userId, "test@test.com", null, true, false, false);

        _identityMock
            .Setup(x => x.ValidateUserAsync(command.Identifier, command.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(identityInfo));

        _identityMock
            .Setup(x => x.GetUserRolesAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<IReadOnlyCollection<string>>(Array.Empty<string>()));

        _identityMock
            .Setup(x => x.GetUserClaimsAsync(userId))
            .ReturnsAsync(Result.Success<IReadOnlyCollection<Claim>>(Array.Empty<Claim>()));

        _tokenServiceMock
            .Setup(x => x.GenerateAccessToken(
                identityInfo, It.IsAny<IReadOnlyCollection<string>>(), It.IsAny<IReadOnlyCollection<Claim>>()))
            .Returns(new AccessToken("accessToken", DateTimeOffset.UtcNow));

        _tokenServiceMock
            .Setup(x => x.GenerateRefreshToken(userId))
            .Returns(new RefreshTokenEnvelope(userId, "rawRefreshToken123", DateTimeOffset.UtcNow.AddDays(7), true));

        _tokenHasherMock
            .Setup(x => x.Hash("rawRefreshToken123"))
            .Returns("hashedRefreshToken123");

        _unitMock
            .Setup(x => x.RefreshTokens.RevokeAllAsync(
                userId,
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _unitMock
            .Setup(x => x.RefreshTokens.StoreAsync(
                userId,
                "hashedRefreshToken123",
                It.IsAny<DateTimeOffset>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        Result<LoginUserCommandResult> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AccessToken.Should().Be("accessToken");
        result.Value.RefreshToken.Should().Be("rawRefreshToken123");

        _identityMock.Verify(
            x => x.ValidateUserAsync(command.Identifier, command.Password, It.IsAny<CancellationToken>()),
            Times.Once);

        _unitMock.Verify(
            x => x.RefreshTokens.StoreAsync(
                userId, "hashedRefreshToken123",
                It.IsAny<DateTimeOffset>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
