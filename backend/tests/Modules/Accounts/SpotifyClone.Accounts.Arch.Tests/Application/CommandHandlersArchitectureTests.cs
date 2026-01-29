using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Accounts.Application;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Accounts.Arch.Tests.Application;

public sealed class CommandHandlersArchitectureTests
{
    [Fact]
    public void CommandHandlers_Should_BeSealed()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommandHandler))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlers_Should_NotBeAbstract()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommandHandler))
            .Should()
            .NotBeAbstract()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlers_Should_BeInternal()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommandHandler))
            .Should()
            .NotBePublic()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlers_Should_HaveCommandHandlerPostfix()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IBaseCommandHandler))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlers_Should_InheritFromIBaseCommandHandlerInterface()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith("CommandHandler")
            .Should()
            .ImplementInterface(typeof(IBaseCommandHandler))
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
