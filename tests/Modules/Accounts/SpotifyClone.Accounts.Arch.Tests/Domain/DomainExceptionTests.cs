using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Arch.Tests.Domain;

public sealed class DomainExceptionTests
{
    private readonly Assembly _domainAssembly = typeof(InvalidDisplayNameDomainException).Assembly;

    [Fact]
    public void DomainExceptions_Should_BeSealed()
    {
        // Arrange & Act
        TestResult result = Types.InAssembly(_domainAssembly)
            .That()
            .Inherit(typeof(DomainExceptionBase))
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainExceptions_Should_HaveDomainExceptionPostfix()
    {
        // Arrange & Act
        TestResult result = Types.InAssembly(_domainAssembly)
            .That()
            .Inherit(typeof(DomainExceptionBase))
            .And()
            .AreNotAbstract()
            .Should()
            .HaveNameEndingWith("DomainException")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
