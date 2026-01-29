using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Accounts.Application;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Arch.Tests.Application;

public sealed class CommandsArchitectureTests
{
    [Fact]
    public void Commands_Should_BeSealed()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommand))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Commands_Should_NotBeAbstract()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommand))
            .Should()
            .NotBeAbstract()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Commands_Should_BePublic()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommand))
            .Should()
            .BePublic()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Commands_Should_HaveCommandPostfix()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommand))
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Commands_Should_InheritFromIBaseCommandInterface()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith("Command")
            .Should()
            .ImplementInterface(typeof(IBaseCommand))
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
