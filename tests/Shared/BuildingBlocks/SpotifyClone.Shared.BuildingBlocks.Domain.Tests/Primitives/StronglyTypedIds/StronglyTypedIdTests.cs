using System;
using System.Reflection;
using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

public sealed class StronglyTypedIdTests
{
    [Fact]
    public void New_Should_AssignValue()
    {
        // Arrange & Act
        var testId = TestId.New();

        // Assert
        testId.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void From_Should_AssignProvidedValue()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var testId = TestId.From(guid);

        // Assert
        testId.Value.Should().Be(guid);
    }

    [Fact]
    public void StronglyTypedIds_Should_BeEqual_When_ValuesAreEqual()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var testId1 = TestId.From(guid);
        var testId2 = TestId.From(guid);

        // Act
        bool areEqual = testId1 == testId2;

        // Assert
        areEqual.Should().BeTrue();
    }

    [Fact]
    public void StronglyTypedIds_Should_NotBeEqual_When_ValuesAreNotEqual()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var testId1 = TestId.From(guid1);
        var testId2 = TestId.From(guid2);

        // Act
        bool areEqual = testId1 == testId2;

        // Assert
        areEqual.Should().BeFalse();
    }

    [Fact]
    public void StronglyTypedIds_Should_HaveSameHashCode_When_ValuesAreEqual()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var testId1 = TestId.From(guid);
        var testId2 = TestId.From(guid);

        // Assert
        testId1.GetHashCode().Should().Be(testId2.GetHashCode());
    }

    [Fact]
    public void StronglyTypedIds_Should_HaveDifferentHashCodes_When_ValuesAreNotEqual()
    {
        // Arrange & Act
        var testId1 = TestId.New();
        var testId2 = TestId.New();

        // Assert
        testId1.GetHashCode().Should().NotBe(testId2.GetHashCode());
    }

    [Fact]
    public void DifferentTypes_Should_NotBeEqual_When_ValuesAreSame()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var testId = TestId.From(guid);
        var otherTestId = OtherTestId.From(guid);

        // Act
        bool areEqual = testId == otherTestId;

        // Assert
        areEqual.Should().BeFalse();
    }

    [Fact]
    public void DifferentTypes_Should_HaveDifferentHashCodes_When_ValuesAreSame()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var testId = TestId.From(guid);
        var otherTestId = OtherTestId.From(guid);

        // Act
        int hashCode1 = testId.GetHashCode();
        int hashCode2 = otherTestId.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }
}
