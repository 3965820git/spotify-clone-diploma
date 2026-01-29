using System.Reflection;
using FluentAssertions;
using MediatR;
using NetArchTest.Rules;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Application;

public sealed class PipelineBehaviorArchitectureTests
{
    [Fact]
    public void PipelineBehaviors_Should_BeSealed()
    {
        // Arrange
        Assembly applicationAssembly = typeof(ExceptionHandlingPipelineBehavior<,>).Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IPipelineBehavior<,>))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PipelineBehaviors_Should_NotBeAbstract()
    {
        // Arrange
        Assembly applicationAssembly = typeof(ExceptionHandlingPipelineBehavior<,>).Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IPipelineBehavior<,>))
            .Should()
            .NotBeAbstract()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PipelineBehaviors_Should_HavePipelineBehaviorPostfix()
    {
        // Arrange
        Assembly applicationAssembly = typeof(ExceptionHandlingPipelineBehavior<,>).Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IPipelineBehavior<,>))
            .Should()
            .HaveNameEndingWith("PipelineBehavior")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PipelineBehaviors_Should_Implement_IPipelineBehaviorInterface()
    {
        // Arrange
        Assembly applicationAssembly = typeof(ExceptionHandlingPipelineBehavior<,>).Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith("PipelineBehavior")
            .Should()
            .ImplementInterface(typeof(IPipelineBehavior<,>))
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PipelineBehaviors_Should_NotReference_DomainLayer()
    {
        // Arrange
        Assembly applicationAssembly = typeof(ExceptionHandlingPipelineBehavior<,>).Assembly;
        var domainAssembly = Assembly.Load("SpotifyClone.Shared.BuildingBlocks.Domain");

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IPipelineBehavior<,>))
            .ShouldNot()
            .HaveDependencyOn(domainAssembly.GetName().Name!)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PipelineBehaviors_Should_NotReference_InfrastructureLayer()
    {
        // Arrange
        Assembly applicationAssembly = typeof(ExceptionHandlingPipelineBehavior<,>).Assembly;
        var infrastructureAssembly = Assembly.Load("SpotifyClone.Shared.BuildingBlocks.Infrastructure");

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(IPipelineBehavior<,>))
            .ShouldNot()
            .HaveDependencyOn(infrastructureAssembly.GetName().Name!)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
