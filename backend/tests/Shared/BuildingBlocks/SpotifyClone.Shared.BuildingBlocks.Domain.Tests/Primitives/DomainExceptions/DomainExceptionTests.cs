using FluentAssertions;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.DomainExceptions;

public sealed class DomainExceptionTests
{
    [Fact]
    public void Constructor_Should_ThrowArgumentException_When_MessageIsNull()
    {
        // Arrange & Act
        Action actNull = () => new NullTestDomainException();

        // Assert
        actNull.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentException_When_MessageIsEmpty()
    {
        // Arrange & Act
        Action actEmpty = () => new EmptyTestDomainException();

        // Assert
        actEmpty.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentException_When_MessageIsWhitespace()
    {
        // Arrange & Act
        Action actWhitespace = () => new WhitespaceTestDomainException();

        // Assert
        actWhitespace.Should().Throw<ArgumentException>();
    }
}
