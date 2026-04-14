using System.Reflection;
using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Tests.Results;

public sealed class ResultTests
{
    [Fact]
    public void Success_Should_CreateSuccessfulResultWithNoErrors()
    {
        // Arrange & Act
        var result = Result.Success();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void SuccessGeneric_Should_CreateSuccessfulResultWithValueAndNoErrors()
    {
        // Arrange
        string expectedValue = "Hello";

        // Act
        var result = Result.Success(expectedValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Errors.Should().BeEmpty();
        result.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void ImplicitConversionFromValueToResult_Should_CreateSuccessfulResult()
    {
        // Arrange
        string value = "Implicit success";

        // Act
        Result<string> result = value;

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Should().Be(value);
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void FailureWithErrors_Should_CreateFailedResultWithProvidedErrors()
    {
        // Arrange
        var error1 = new Error("ERR001", "First error");
        var error2 = new Error("ERR002", "Second error");

        // Act
        var result = Result.Failure(error1, error2);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainInOrder(error1, error2);
        result.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void FailureWithNullErrors_Should_UseUnknownError()
    {
        // Arrange & Act
        var result = Result.Failure(null!);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().Be(CommonErrors.Unknown);
    }

    [Fact]
    public void FailureWithNoErrors_Should_UseUnknownError()
    {
        // Act
        var result = Result.Failure();

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().Be(CommonErrors.Unknown);
    }

    [Fact]
    public void FailureGeneric_Should_CreateFailedResultWithDefaultValueAndErrors()
    {
        // Arrange
        var error = new Error("ERR003", "Something went wrong");

        // Act
        var result = Result.Failure<string>(error);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void ConstructorSuccessWithErrorsProvided_Should_IgnoreErrorsAndSetEmptyArray()
    {
        // Arrange
        var error = new Error("IGNORED", "This should be ignored");

        // Act
        Type resultType = typeof(Result);
        var successResult = (Result)Activator.CreateInstance(
            resultType,
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            args: new object[] { true, new[] { error } },
            culture: null)!;

        // Assert
        successResult.IsSuccess.Should().BeTrue();
        successResult.IsFailure.Should().BeFalse();
        successResult.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ConstructorFailureWithNoErrors_Should_DefaultToUnknownError()
    {
        // Arrange
        Type resultType = typeof(Result);

        // Act
        var failureResult = (Result)Activator.CreateInstance(
            resultType,
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            args: new object[] { false, Array.Empty<Error>() },
            culture: null)!;

        // Assert
        failureResult.IsSuccess.Should().BeFalse();
        failureResult.IsFailure.Should().BeTrue();
        failureResult.Errors.Should().ContainSingle().Which.Should().Be(CommonErrors.Unknown);
    }
}
