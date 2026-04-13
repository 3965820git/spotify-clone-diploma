using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Events;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Albums;

internal sealed class TrackRemovedFromAlbumDomainEventHandler(
    ICatalogUnitOfWork unit,
    ILogger<TrackRemovedFromAlbumDomainEventHandler> logger)
    : INotificationHandler<TrackRemovedFromAlbumDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ILogger<TrackRemovedFromAlbumDomainEventHandler> _logger = logger;

    public async Task Handle(
        TrackRemovedFromAlbumDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
                notification.AlbumId, cancellationToken);
        if (album is null)
        {
            _logger.LogError(
                "Album {AlbumId} not found while unattaching track {TrackId} from it",
                notification.AlbumId.Value,
                notification.TrackId.Value);
            return;
        }

        Track? track = await _unit.Tracks.GetByIdAsync(
                notification.TrackId, cancellationToken);
        if (track is null)
        {
            _logger.LogError(
                "Track {TrackId} not found while unattaching it from Album {AlbumId}",
                notification.TrackId.Value,
                notification.AlbumId.Value);
            return;
        }

        track.Archive();

        await _unit.CommitAsync(cancellationToken);
    }
}
