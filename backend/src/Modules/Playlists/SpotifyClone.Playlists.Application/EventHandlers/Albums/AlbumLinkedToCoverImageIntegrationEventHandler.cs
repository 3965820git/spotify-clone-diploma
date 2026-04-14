using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;

namespace SpotifyClone.Playlists.Application.EventHandlers.Albums;

internal sealed class AlbumLinkedToCoverImageIntegrationEventHandler(
    IPlaylistsUnitOfWork unit,
    ILogger<AlbumLinkedToCoverImageIntegrationEventHandler> logger)
    : INotificationHandler<AlbumLinkedToCoverImageIntegrationEvent>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ILogger<AlbumLinkedToCoverImageIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        AlbumLinkedToCoverImageIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        if (!await _unit.TrackReferences.ExistsAsync(notification.Tracks, cancellationToken))
        {
            _logger.LogError("Track was not found in the Playlists module.");
            throw new InvalidOperationException($"Track was not found in the Playlists module.");
        }

        foreach (Guid trackId in notification.Tracks)
        {
            await _unit.TrackReferences.LinkCoverAsync(trackId, notification.CoverImageId, cancellationToken);
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
