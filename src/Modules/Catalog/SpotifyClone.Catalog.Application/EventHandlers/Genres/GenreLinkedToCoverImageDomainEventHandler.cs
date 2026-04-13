using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;

namespace SpotifyClone.Catalog.Application.EventHandlers.Genres;

internal sealed class GenreLinkedToCoverImageDomainEventHandler(
    ICatalogUnitOfWork unit)
    : INotificationHandler<GenreLinkedToCoverImageDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task Handle(
        GenreLinkedToCoverImageDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new ImageLinkAddedIntegrationEvent(
                notification.ImageId.Value);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
