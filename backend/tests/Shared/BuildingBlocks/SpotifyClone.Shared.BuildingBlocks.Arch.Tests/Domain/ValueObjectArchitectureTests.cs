using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Domain;

public sealed class ValueObjectArchitectureTests
{
    [Fact]
    public void ValueObject_Should_BeAbstractClass()
    {
        // Arrange
        Type valueObjectType = typeof(ValueObject);

        // Act
        bool isAbstractClass = valueObjectType.IsAbstract && valueObjectType.IsClass;

        // Assert
        isAbstractClass.Should().BeTrue();
    }
}
