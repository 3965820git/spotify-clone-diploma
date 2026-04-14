using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;

namespace SpotifyClone.Catalog.Application.EventHandlers.Artists;

internal sealed class ArtistLinkedToAvatarImageDomainEventHandler(
    ICatalogUnitOfWork unit)
    : INotificationHandler<ArtistLinkedToAvatarImageDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task Handle(
        ArtistLinkedToAvatarImageDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new ImageLinkAddedIntegrationEvent(
                notification.ImageId.Value);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
