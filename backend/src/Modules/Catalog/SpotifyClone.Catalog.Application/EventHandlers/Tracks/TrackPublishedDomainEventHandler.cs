using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Tracks;

internal sealed class TrackPublishedDomainEventHandler(
    ICatalogUnitOfWork unit)
    : INotificationHandler<TrackPublishedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task Handle(
        TrackPublishedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new TrackPublishedIntegrationEvent(
            notification.TrackId.Value);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
