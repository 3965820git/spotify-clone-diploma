using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Tests.Behaviors;

public sealed class RequestLoggingPipelineBehaviorTests
{
    private readonly Mock<ILogger<RequestLoggingPipelineBehavior<TestCommand, Result>>> _loggerMock = new();
    private readonly RequestLoggingPipelineBehavior<TestCommand, Result> _behavior;

    public RequestLoggingPipelineBehaviorTests()
        => _behavior = new RequestLoggingPipelineBehavior<TestCommand, Result>(_loggerMock.Object);

    [Fact]
    public async Task Handle_Should_CallNextHandler()
    {
        // Arrange
        var request = new TestCommand();

        bool nextCalled = false;

        RequestHandlerDelegate<Result> next = (CancellationToken _) =>
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
    public async Task Handle_Should_LogStartOfRequest()
    {
        // Arrange
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = (CancellationToken _) => Task.FromResult(Result.Success());

        // Act
        await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Processing request TestCommand")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_LogSuccessWhenResultIsSuccess()
    {
        // Arrange
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = (CancellationToken _) => Task.FromResult(Result.Success());

        // Act
        await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Completed request TestCommand successfully")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_LogWarningWhenResultIsFailure()
    {
        // Arrange
        var request = new TestCommand();
        RequestHandlerDelegate<Result> next = _ => Task.FromResult(
            Result.Failure(CommonErrors.Unknown));

        // Act
        await _behavior.Handle(request, next, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
