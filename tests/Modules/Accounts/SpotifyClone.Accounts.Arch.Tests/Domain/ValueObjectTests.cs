using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Accounts.Domain.Aggregates.Users.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Arch.Tests.Domain;

public sealed class ValueObjectTests
{
    private readonly Assembly _domainAssembly = typeof(AvatarImage).Assembly;

    [Fact]
    public void ValueObjects_Should_BeSealed()
    {
        // Arrange & Act
        TestResult result = Types.InAssembly(_domainAssembly)
            .That()
            .Inherit(typeof(ValueObject))
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
