using MediatR;

namespace SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

public abstract record DomainEvent : INotification
{
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
}
