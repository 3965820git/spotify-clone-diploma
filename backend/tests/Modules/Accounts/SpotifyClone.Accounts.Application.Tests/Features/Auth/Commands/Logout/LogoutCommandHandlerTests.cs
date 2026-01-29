using Moq;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.Logout;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Tests.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandlerTests
{
    private readonly Mock<IAccountsUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICurrentUser> _currentUserMock;
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepoMock;

    private readonly LogoutCommandHandler _handler;

    public LogoutCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IAccountsUnitOfWork>();
        _currentUserMock = new Mock<ICurrentUser>();
        _refreshTokenRepoMock = new Mock<IRefreshTokenRepository>();

        _unitOfWorkMock
            .Setup(u => u.RefreshTokens)
            .Returns(_refreshTokenRepoMock.Object);

        _handler = new LogoutCommandHandler(
            _unitOfWorkMock.Object,
            _currentUserMock.Object);
    }

    [Fact]
    public async Task Handle_Should_RevokeAllRefreshTokens_And_ReturnSuccess()
    {
        // Arrange
        var userId = UserId.From(Guid.NewGuid());

        _currentUserMock
            .Setup(c => c.UserId)
            .Returns(userId);

        _refreshTokenRepoMock
            .Setup(r => r.RevokeAllAsync(
                userId,
                null,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var command = new LogoutCommand();

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        _refreshTokenRepoMock.Verify(
            r => r.RevokeAllAsync(
                userId,
                null,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
