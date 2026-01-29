using System.Reflection;
using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Domain;

public sealed class StronglyTypedIdArchitectureTests
{
    [Fact]
    public void StronglyTypedId_Should_BeAbstractClass()
    {
        // Arrange
        Type stronglyTypedIdType = typeof(StronglyTypedId<>);

        // Act
        bool isAbstract = stronglyTypedIdType.IsAbstract;

        // Assert
        Assert.True(isAbstract);
    }

    [Fact]
    public void StronglyTypedId_Should_Have_ProtectedConstructor()
    {
        // Arrange
        Type stronglyTypedIdType = typeof(StronglyTypedId<>);
        ConstructorInfo[] constructors = stronglyTypedIdType.
            GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        // Act
        ConstructorInfo? protectedConstructor = constructors.FirstOrDefault(c => c.IsFamily);

        // Assert
        protectedConstructor.Should().NotBeNull();
    }

    [Fact]
    public void StronglyTypedId_Should_InheritFromValueObject()
    {
        // Arrange
        Type stronglyTypedIdType = typeof(StronglyTypedId<>);
        Type valueObjectType = typeof(ValueObject);

        // Act
        bool isSubclass = stronglyTypedIdType.IsSubclassOf(valueObjectType);

        // Assert
        isSubclass.Should().BeTrue();
    }

    [Fact]
    public void SronglyTypedId_Should_Have_Value_Property_Of_GenericType()
    {
        // Arrange
        Type stronglyTypedIdType = typeof(StronglyTypedId<>);
        PropertyInfo? valueProperty = stronglyTypedIdType.GetProperty("Value");

        // Act & Assert
        valueProperty.Should().NotBeNull();
        valueProperty!.PropertyType.IsGenericParameter.Should().BeTrue();
    }
}
