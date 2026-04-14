using FluentAssertions;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.DomainEvents;

public sealed class DomainEventTests
{
    [Fact]
    public void DomainEvent_Should_SetOccurredOnToUtcNow()
    {
        // Arrange & Act
        var domainEvent = new TestDomainEvent(42);

        // Assert
        domainEvent.OccurredOn.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Constructor_Should_AssignData()
    {
        // Arrange
        int expectedData = 99;

        // Act
        var domainEvent = new TestDomainEvent(expectedData);

        // Assert
        domainEvent.Value.Should().Be(expectedData);
    }

    [Fact]
    public void DifferentTypes_Should_NotBeEqual_EvenIfPropertiesAreSame()
    {
        // Arrange
        var event1 = new TestDomainEvent(100);
        var event2 = new OtherTestDomainEvent(100);

        // Act
        bool areEqual = event1.Equals(event2);

        // Assert
        areEqual.Should().BeFalse();
    }
}
