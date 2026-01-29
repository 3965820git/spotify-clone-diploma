using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Domain;

public sealed class AggregateRootArchitectureTests
{
    // This method will be moved to different test modules once we have some REAL code that implements AggregateRoot.
    [Fact]
    public void AggregateRoots_Should_Not_Be_Extensible_Accidentally()
    {
        // Arrange
        Assembly domainAssembly = typeof(AggregateRoot<,>).Assembly;

        // Act
        TestResult result = Types.InAssembly(domainAssembly)
            .That()
            .Inherit(typeof(AggregateRoot<,>))
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
