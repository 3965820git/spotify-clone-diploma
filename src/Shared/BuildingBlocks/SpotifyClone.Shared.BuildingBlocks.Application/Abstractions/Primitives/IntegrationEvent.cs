using MediatR;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

public abstract record IntegrationEvent : INotification
{
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
}
