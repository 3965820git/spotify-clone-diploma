using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Domain;

public sealed class EntityArchitectureTests
{
    [Fact]
    public void Entity_Should_BeAbstractClass()
    {
        // Arrange
        Type entityType = typeof(Entity<,>);

        // Act
        bool isAbstractClass = entityType.IsAbstract && entityType.IsClass;

        // Assert
        isAbstractClass.Should().BeTrue();
    }

    // This method will be moved to different test modules once we have some REAL code that implements Entity.
    [Fact]
    public void Entities_Should_BeSealed()
    {
        // Arrange
        Assembly domainAssembly = typeof(Entity<,>).Assembly;

        // Act
        TestResult result = Types.InAssembly(domainAssembly)
            .That()
            .Inherit(typeof(Entity<,>))
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
