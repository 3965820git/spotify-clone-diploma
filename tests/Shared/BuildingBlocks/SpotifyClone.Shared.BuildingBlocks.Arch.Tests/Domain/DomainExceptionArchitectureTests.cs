using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Domain;

public sealed class DomainExceptionArchitectureTests
{
    [Fact]
    public void DomainExceptionBase_Should_BeAbstractClass()
    {
        // Arrange
        Type domainExceptionType = typeof(DomainExceptionBase);

        // Act
        bool isAbstractClass = domainExceptionType.IsAbstract && domainExceptionType.IsClass;

        // Assert
        isAbstractClass.Should().BeTrue();
    }

    [Fact]
    public void DomainExceptionBase_Should_InheritFrom_Exception()
    {
        // Arrange
        Type domainExceptionType = typeof(DomainExceptionBase);
        Type exceptionType = typeof(Exception);

        // Act
        bool inheritsFromException = exceptionType.IsAssignableFrom(domainExceptionType);

        // Assert
        inheritsFromException.Should().BeTrue();
    }
}
