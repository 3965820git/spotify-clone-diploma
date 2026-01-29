using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.StronglyTypedIds;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Tests.Primitives.AggregateRoots;

internal sealed class OtherTestAggregate : AggregateRoot<OtherTestId, Guid>
{
    public OtherTestAggregate(OtherTestId id)
        : base(id)
    {
    }

    public void TriggerEvent(DomainEvent domainEvent)
        => RaiseDomainEvent(domainEvent);

    public void Clear()
        => ClearDomainEvents();
}
