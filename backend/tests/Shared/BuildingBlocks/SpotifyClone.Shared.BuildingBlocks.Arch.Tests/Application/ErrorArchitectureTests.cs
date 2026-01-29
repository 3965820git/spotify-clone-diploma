using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Application;

public sealed class ErrorArchitectureTests
{
    [Fact]
    public void DomainEvents_Should_BeAbstractClass()
    {
        // Arrange
        Type errorType = typeof(Error);

        // Act
        bool isNotAbstractClass = !errorType.IsAbstract && errorType.IsClass;

        // Assert
        isNotAbstractClass.Should().BeTrue();
    }

    [Fact]
    public void Error_Should_BeSealed()
    {
        // Arrange
        Type errorType = typeof(Error);

        // Act
        bool isSealed = errorType.IsSealed;

        // Assert
        isSealed.Should().BeTrue();
    }
}
