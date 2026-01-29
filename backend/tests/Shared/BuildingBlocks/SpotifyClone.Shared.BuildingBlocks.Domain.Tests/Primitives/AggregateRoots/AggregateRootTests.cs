using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.DomainEvents;
using SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.AggregateRoots;

public sealed class AggregateRootTests
{
    [Fact]
    public void AggregatesWithSameId_Should_BeEqual()
    {
        // Arrange
        var id = TestId.New();
        var aggregate1 = new TestAggregate(id);
        var aggregate2 = new TestAggregate(id);

        // Act
        bool areEqual = aggregate1 == aggregate2;

        // Assert
        areEqual.Should().BeTrue();
    }

    [Fact]
    public void AggregatesWithDifferentIds_Should_NotBeEqual()
    {
        // Arrange
        var aggregate1 = new TestAggregate(TestId.New());
        var aggregate2 = new TestAggregate(TestId.New());

        // Act
        bool areEqual = aggregate1 == aggregate2;

        // Assert
        areEqual.Should().BeFalse();
    }

    [Fact]
    public void AggregatesWithSameId_Should_HaveSameHashCode()
    {
        // Arrange
        var id = TestId.New();
        var aggregate1 = new TestAggregate(id);
        var aggregate2 = new TestAggregate(id);

        // Act
        int hashCode1 = aggregate1.GetHashCode();
        int hashCode2 = aggregate2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void AggregatesWithDifferentIds_Should_HaveDifferentHashCodes()
    {
        // Arrange
        var aggregate1 = new TestAggregate(TestId.New());
        var aggregate2 = new TestAggregate(TestId.New());

        // Act
        int hashCode1 = aggregate1.GetHashCode();
        int hashCode2 = aggregate2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [Fact]
    public void AggregatesOfDifferentTypesWithSameId_Should_NotBeEqual()
    {
        // Arrange
        var id = TestId.New();
        var aggregate1 = new TestAggregate(id);
        var aggregate2 = new OtherTestAggregate(OtherTestId.From(id.Value));

        // Act
        bool areEqual = aggregate1.Equals(aggregate2);

        // Assert
        areEqual.Should().BeFalse();
    }

    [Fact]
    public void AggregatesWithNullId_Should_ThrowException()
    {
        // Arrange, Act
        Action act = () => new TestAggregate(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void DomainEvents_Should_BeReadOnly()
    {
        // Arrange
        var aggregate = new TestAggregate(TestId.New());

        // Act
        IReadOnlyCollection<DomainEvent> events = aggregate.DomainEvents;

        // Assert
        events.Should().BeAssignableTo<IReadOnlyCollection<DomainEvent>>();
    }

    [Fact]
    public void RaiseDomainEvent_Should_AddEventToDomainEvents()
    {
        // Arrange
        var aggregate = new TestAggregate(TestId.New());
        var domainEvent = new TestDomainEvent(42);
        var otherDomainEvent = new OtherTestDomainEvent(84);

        // Act
        aggregate.TriggerEvent(domainEvent);
        aggregate.TriggerEvent(otherDomainEvent);

        // Assert
        aggregate.DomainEvents
            .Should()
            .Contain(domainEvent)
            .And
            .Contain(otherDomainEvent);
    }

    [Fact]
    public void ClearDomainEvents_Should_RemoveAllEvents()
    {
        // Arrange
        var aggregate = new TestAggregate(TestId.New());
        aggregate.TriggerEvent(new TestDomainEvent(1));
        aggregate.TriggerEvent(new OtherTestDomainEvent(2));

        // Act
        aggregate.Clear();

        // Assert
        aggregate.DomainEvents.Should().BeEmpty();
    }
}
