using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;

namespace SpotifyClone.Catalog.Application.EventHandlers.Moods;

internal sealed class MoodLinkedToCoverImageDomainEventHandler(
    ICatalogUnitOfWork unit)
    : INotificationHandler<MoodLinkedToCoverImageDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task Handle(
        MoodLinkedToCoverImageDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new ImageLinkAddedIntegrationEvent(
                notification.ImageId.Value);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
