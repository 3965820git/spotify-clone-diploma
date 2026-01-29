using FluentAssertions;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Domain.Tests.Aggregates.Users.Enums;

public sealed class GenderTests
{
    [Fact]
    public void Gender_Should_InheritFromValueObject()
    {
        // Arrange & Act
        Gender gender = Gender.Male;

        // Assert
        gender.Should().BeAssignableTo<ValueObject>();
    }

    [Theory]
    [InlineData("  Male ")]
    [InlineData("  Female ")]
    [InlineData("  NonBinary ")]
    [InlineData("  NotSpecified ")]
    public void From_Should_NormalizeValue(string value)
    {
        // Arrange & Act
        var gender = Gender.From(value);

        // Assert
        gender.Value.Should().Be(value.Trim());
    }

    [Theory]
    [InlineData("Male")]
    [InlineData("Female")]
    [InlineData("NonBinary")]
    [InlineData("NotSpecified")]
    public void From_Should_CreateInstance_When_ValueIsSupported(string value)
    {
        // Arrange & Act
        Func<Gender> result = () => Gender.From(value);

        // Assert
        result.Should().NotThrow<InvalidGenderDomainException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void From_Should_ThrowException_When_ValueIsNullOrWhitespace(string? invalidValue)
    {
        // Arrange & Act
        Func<Gender> result = () => Gender.From(invalidValue!);

        // Assert
        result.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("Transgender")]
    public void From_Should_ThrowException_When_ValueIsNotSupported(string unsupportedValue)
    {
        // Arrange & Act
        Func<Gender> result = () => Gender.From(unsupportedValue);

        // Assert
        result.Should().Throw<InvalidGenderDomainException>();
    }
}
