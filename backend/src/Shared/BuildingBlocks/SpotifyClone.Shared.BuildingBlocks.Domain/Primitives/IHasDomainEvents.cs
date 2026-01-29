namespace SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
