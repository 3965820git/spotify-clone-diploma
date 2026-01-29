using System.Reflection;
using FluentAssertions;
using FluentValidation;
using NetArchTest.Rules;
using SpotifyClone.Accounts.Application;

namespace SpotifyClone.Accounts.Arch.Tests.Application;

public sealed class FluentValidatorsArchitectureTests
{
    [Fact]
    public void Validators_Should_BeSealed()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Validators_Should_NotBeAbstract()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .NotBeAbstract()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Validators_Should_BePublic()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .BePublic()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Validators_Should_HaveCommandPostfix()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Validators_Should_InheritFromAbstractValidatorAbstractClass()
    {
        // Arrange
        Assembly applicationAssembly = AccountsApplicationAssemblyReference.Assembly;

        // Act
        TestResult result = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith("Validator")
            .Should()
            .Inherit(typeof(AbstractValidator<>))
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
