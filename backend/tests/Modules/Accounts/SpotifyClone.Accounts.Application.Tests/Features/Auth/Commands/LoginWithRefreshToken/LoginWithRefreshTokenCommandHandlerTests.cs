using FluentAssertions;
using Moq;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithRefreshToken;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Tests.Features.Auth.Commands.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenCommandHandlerTests
{
    private readonly Mock<IAccountsUnitOfWork> _unit = new();
    private readonly Mock<IRefreshTokenRepository> _refreshTokens = new();
    private readonly Mock<ITokenService> _tokenService = new();
    private readonly Mock<ITokenHasher> _tokenHasher = new();
    private readonly Mock<IIdentityService> _identity = new();

    private readonly LoginWithRefreshTokenCommandHandler _handler;

    public LoginWithRefreshTokenCommandHandlerTests()
    {
        _unit.SetupGet(x => x.RefreshTokens)
             .Returns(_refreshTokens.Object);

        _handler = new LoginWithRefreshTokenCommandHandler(
            _unit.Object,
            _tokenService.Object,
            _tokenHasher.Object,
            _identity.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_RefreshTokenLookupFails()
    {
        var command = new LoginWithRefreshTokenCommand("raw-token");
        string hash = "hash";

        _tokenHasher.Setup(x => x.Hash("raw-token"))
                    .Returns(hash);

        Error error = RefreshTokenErrors.InvalidToken;

        _refreshTokens
            .Setup(x => x.GetByTokenHashAsync(hash, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<RefreshTokenEnvelope>(error));

        Result<LoginWithRefreshTokenCommandResult> result =
            await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e == error);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_IdentityLookupFails()
    {
        var command = new LoginWithRefreshTokenCommand("raw-token");
        string hash = "hash";
        var userId = UserId.New();

        var storedToken = new RefreshTokenEnvelope(
            UserId: userId,
            RawToken: "ignored",
            ExpiresAt: DateTimeOffset.UtcNow.AddDays(10),
            IsActive: true);

        _tokenHasher.Setup(x => x.Hash("raw-token"))
                    .Returns(hash);

        _refreshTokens
            .Setup(x => x.GetByTokenHashAsync(hash, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(storedToken));

        Error error = IdentityUserErrors.NotFound;

        _identity
            .Setup(x => x.GetUserInfoAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<IdentityUserInfo>(error));

        Result<LoginWithRefreshTokenCommandResult> result =
            await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e == error);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_RevokeFails()
    {
        var command = new LoginWithRefreshTokenCommand("raw-token");
        string hash = "old-hash";
        string newHash = "new-hash";
        var userId = UserId.New();

        var storedToken =
            new RefreshTokenEnvelope(userId, "ignored", DateTimeOffset.UtcNow.AddDays(10), true);

        var identity =
            new IdentityUserInfo(userId, "test@test.com", true, false);

        _tokenHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns<string>(s => s == "raw-token" ? hash : newHash);

        _refreshTokens
            .Setup(x => x.GetByTokenHashAsync(hash, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(storedToken));

        _identity
            .Setup(x => x.GetUserInfoAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(identity));

        _tokenService
            .Setup(x => x.GenerateAccessToken(
                identity,
                It.IsAny<IReadOnlyCollection<string>>()))
            .Returns(new AccessToken("access", DateTimeOffset.UtcNow.AddMinutes(5)));

        _tokenService
            .Setup(x => x.GenerateRefreshToken(userId))
            .Returns(new RefreshTokenEnvelope(
                userId,
                "new-raw",
                DateTimeOffset.UtcNow.AddDays(30),
                true));

        Error error = RefreshTokenErrors.InvalidToken;

        _refreshTokens
            .Setup(x => x.RevokeAsync(hash, newHash, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(error));

        Result<LoginWithRefreshTokenCommandResult> result =
            await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e == error);
    }

    [Fact]
    public async Task Handle_Should_ReturnNewTokens_When_RefreshTokenIsValid()
    {
        var command = new LoginWithRefreshTokenCommand("raw-token");

        string oldHash = "old-hash";
        string newHash = "new-hash";
        var userId = UserId.New();

        var storedToken = new RefreshTokenEnvelope(userId, "ignored", DateTimeOffset.UtcNow.AddDays(10), true);

        var identity = new IdentityUserInfo(userId, "test@test.com", true, false);

        var accessToken = new AccessToken("access-token", DateTimeOffset.UtcNow.AddMinutes(5));
        var newRefreshToken = new RefreshTokenEnvelope(
            userId,
            "new-refresh",
            DateTimeOffset.UtcNow.AddDays(30),
            true);

        _tokenHasher.Setup(x => x.Hash("raw-token")).Returns(oldHash);
        _tokenHasher.Setup(x => x.Hash("new-refresh")).Returns(newHash);

        _refreshTokens
            .Setup(x => x.GetByTokenHashAsync(oldHash, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(storedToken));

        _identity
            .Setup(x => x.GetUserInfoAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(identity));

        _tokenService
            .Setup(x => x.GenerateAccessToken(identity, It.IsAny<IReadOnlyCollection<string>>()))
            .Returns(accessToken);

        _tokenService
            .Setup(x => x.GenerateRefreshToken(userId))
            .Returns(newRefreshToken);

        _refreshTokens
            .Setup(x => x.RevokeAsync(oldHash, newHash, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _refreshTokens
            .Setup(x => x.StoreAsync(userId, newHash, newRefreshToken.ExpiresAt, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        Result<LoginWithRefreshTokenCommandResult> result =
            await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.AccessToken.Should().Be(accessToken.RawToken);
        result.Value.RefreshToken.Should().Be(newRefreshToken.RawToken);
    }
}
