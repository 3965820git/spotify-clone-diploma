using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

namespace SpotifyClone.Playlists.Application.EventHandlers.Tracks;

internal sealed class TrackMarkedAsReadyIntegrationEventHandler(
    IPlaylistsUnitOfWork unit,
    ILogger<TrackMarkedAsReadyIntegrationEventHandler> logger)
    : INotificationHandler<TrackMarkedAsReadyIntegrationEvent>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ILogger<TrackMarkedAsReadyIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        TrackMarkedAsReadyIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        if (await _unit.TrackReferences.ExistsAsync(notification.TrackId, cancellationToken))
        {
            _logger.LogError(
                "Track {TrackId} already exists in the Playlists module.",
                notification.TrackId);
            throw new InvalidOperationException(
                $"Track {notification.TrackId} already exists in the Playlists module.");
        }

        await _unit.TrackReferences.AddAsync(
            notification.TrackId,
            cancellationToken);

        await _unit.CommitAsync(cancellationToken);
    }
}
