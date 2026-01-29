using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.Kernel.Arch.Tests.ValueObjects;

public sealed class StronglyTypedIdsArchitectureTests
{
    [Fact]
    public void StronglyTypedIds_Should_HaveIdPostfix()
    {
        // Arrange
        Assembly domainAssembly = typeof(StronglyTypedId<>).Assembly;

        // Act
        TestResult result = Types.InAssembly(domainAssembly)
            .That()
            .Inherit(typeof(StronglyTypedId<>))
            .Should()
            .HaveNameEndingWith("Id")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
