using FluentAssertions;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.ValueObjects;

public sealed class ValueObjectTests
{
    [Fact]
    public void Constructor_Should_AssignData()
    {
        // Arrange
        int expectedValue = 99;

        // Act
        var valueObject = new TestValueObject(expectedValue);

        // Assert
        valueObject.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void ValueObjectsWithEqualProperties_Should_BeEqual()
    {
        // Arrange
        var valueObject1 = new TestValueObject(42);
        var valueObject2 = new TestValueObject(42);

        // Act
        bool areEqual = valueObject1 == valueObject2;

        // Assert
        areEqual.Should().BeTrue();
    }

    [Fact]
    public void ValueObjectsWithDifferentProperties_Should_NotBeEqual()
    {
        // Arrange
        var valueObject1 = new TestValueObject(42);
        var valueObject2 = new TestValueObject(100);

        // Act
        bool areEqual = valueObject1 == valueObject2;

        // Assert
        areEqual.Should().BeFalse();
    }

    [Fact]
    public void ValueObjectsWithEqualProperties_Should_HaveSameHashCode()
    {
        // Arrange
        var valueObject1 = new TestValueObject(42);
        var valueObject2 = new TestValueObject(42);

        // Act
        int hashCode1 = valueObject1.GetHashCode();
        int hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void ValueObjectsWithDifferentProperties_Should_HaveDifferentHashCodes()
    {
        // Arrange
        var valueObject1 = new TestValueObject(42);
        var valueObject2 = new TestValueObject(100);

        // Act
        int hashCode1 = valueObject1.GetHashCode();
        int hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [Fact]
    public void DifferentTypesWithSameValues_Should_NotBeEqual()
    {
        // Arrange
        var valueObject1 = new TestValueObject(42);
        var valueObject2 = new OtherTestValueObject(42);

        // Act
        bool areEqual = valueObject1.Equals(valueObject2);

        // Assert
        areEqual.Should().BeFalse();
    }

    [Fact]
    public void DifferentTypesWithSameValues_Should_HaveDifferentHashCodes()
    {
        // Arrange
        var valueObject1 = new TestValueObject(42);
        var valueObject2 = new OtherTestValueObject(42);

        // Act
        int hashCode1 = valueObject1.GetHashCode();
        int hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }
}
