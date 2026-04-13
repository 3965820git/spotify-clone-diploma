using MediatR;
using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;

namespace SpotifyClone.Playlists.Application.EventHandlers.Playlists;

internal sealed class PlaylistLinkedToCoverImageDomainEventHandler(
    IPlaylistsUnitOfWork unit)
    : INotificationHandler<PlaylistLinkedToCoverImageDomainEvent>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;

    public async Task Handle(
        PlaylistLinkedToCoverImageDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new ImageLinkAddedIntegrationEvent(
                notification.ImageId.Value);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
