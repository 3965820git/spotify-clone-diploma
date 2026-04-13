using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Tests.Behaviors;

public sealed class ValidationPipelineBehaviorTests
{
    private readonly Mock<IValidator<TestCommand>> _validatorMock = new();
    private readonly ValidationPipelineBehavior<TestCommand, Result> _behavior;

    public ValidationPipelineBehaviorTests()
        => _behavior = new ValidationPipelineBehavior<TestCommand, Result>(new[] { _validatorMock.Object });

    [Fact]
    public async Task Handle_Should_CallNextWhenNoValidators()
    {
        ValidationPipelineBehavior<TestCommand, Result> emptyBehavior = new(Array.Empty<IValidator<TestCommand>>());

        bool nextCalled = false;
        RequestHandlerDelegate<Result> next = _ =>
        {
            nextCalled = true;
            return Task.FromResult(Result.Success());
        };

        Result result = await emptyBehavior.Handle(new TestCommand(), next, CancellationToken.None);

        nextCalled.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallNextWhenValidationSucceeds()
    {
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestCommand>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        bool nextCalled = false;
        RequestHandlerDelegate<Result> next = _ =>
        {
            nextCalled = true;
            return Task.FromResult(Result.Success());
        };

        Result result = await _behavior.Handle(new TestCommand(), next, CancellationToken.None);

        nextCalled.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureWhenValidationFails()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required"),
            new ValidationFailure("Age", "Age must be > 0")
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestCommand>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult(failures));

        bool nextCalled = false;
        RequestHandlerDelegate<Result> next = _ =>
        {
            nextCalled = true;
            return Task.FromResult(Result.Success());
        };

        Result result = await _behavior.Handle(new TestCommand(), next, CancellationToken.None);

        nextCalled.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Errors.Select(e => e.Code).Should().Contain(["Validation.Name", "Validation.Age"]);
        result.Errors.Select(e => e.Description).Should().Contain(["Name is required", "Age must be > 0"]);
    }

    [Fact]
    public async Task Handle_Should_ThrowWhenRequestIsNull()
        => await Assert.ThrowsAsync<ArgumentNullException>(() =>
        _behavior.Handle(null!, (CancellationToken _) => Task.FromResult(Result.Success()), CancellationToken.None));

    [Fact]
    public async Task Handle_Should_Throw_WhenNextIsNull()
        => await Assert.ThrowsAsync<ArgumentNullException>(() =>
        _behavior.Handle(new TestCommand(), null!, CancellationToken.None));
}
