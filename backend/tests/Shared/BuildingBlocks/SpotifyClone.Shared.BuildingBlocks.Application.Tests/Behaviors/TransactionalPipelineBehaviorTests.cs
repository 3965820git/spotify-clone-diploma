using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Tests.Behaviors;

public sealed class TransactionalPipelineBehaviorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ILogger<TransactionalPipelineBehavior<TestCommand, Result>>> _loggerMock = new();

    private readonly TransactionalPipelineBehavior<TestCommand, Result> _behavior;

    public TransactionalPipelineBehaviorTests()
        => _behavior = new TransactionalPipelineBehavior<TestCommand, Result>(
            _unitOfWorkMock.Object,
            _loggerMock.Object);

    [Fact]
    public async Task Handle_Should_CallNextDelegate()
    {
        // Arrange
        bool nextCalled = false;
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = _ =>
        {
            nextCalled = true;
            return Task.FromResult(Result.Success());
        };

        // Act
        await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        nextCalled.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_NotSaveWhenResultIsFailure()
    {
        // Arrange
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = _ =>
            Task.FromResult(Result.Failure(CommonErrors.Unknown));

        // Act
        Result result = await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_SaveWhenResultIsSuccess()
    {
        // Arrange
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = _ => Task.FromResult(Result.Success());

        // Act
        Result result = await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_LogBeginning_And_CommitForSuccess()
    {
        // Arrange
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = _ => Task.FromResult(Result.Success());

        // Act
        await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Beginning transaction for TestCommand")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);


        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Beginning transaction for TestCommand")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_LogBeginningButNotCommitForFailure()
    {
        // Arrange
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = _ => Task.FromResult(Result.Failure(CommonErrors.Unknown));

        // Act
        await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Beginning transaction")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);


        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Commited transaction")),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Never);
    }
}
