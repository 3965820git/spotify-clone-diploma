using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.DomainEvents;
using SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.AggregateRoots;

internal sealed class TestAggregate : AggregateRoot<TestId, Guid>
{
    public TestAggregate(TestId id)
        : base(id)
    {
    }

    public void TriggerEvent(DomainEvent domainEvent)
        => RaiseDomainEvent(domainEvent);

    public void Clear()
        => ClearDomainEvents();
}
