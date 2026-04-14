using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

namespace SpotifyClone.Playlists.Application.EventHandlers.Tracks;

internal sealed class TrackDeletedIntegrationEventHandler(
    IPlaylistsUnitOfWork unit,
    ILogger<TrackDeletedIntegrationEventHandler> logger)
    : INotificationHandler<TrackDeletedIntegrationEvent>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ILogger<TrackDeletedIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        TrackDeletedIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        if (!await _unit.TrackReferences.ExistsAsync(notification.TrackId, cancellationToken))
        {
            _logger.LogError(
                "Track {TrackId} was not found in the Playlists module.",
                notification.TrackId);
            throw new InvalidOperationException(
                $"Track {notification.TrackId} was not found in the Playlists module.");
        }

        await _unit.TrackReferences.DeleteAsync(notification.TrackId, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
