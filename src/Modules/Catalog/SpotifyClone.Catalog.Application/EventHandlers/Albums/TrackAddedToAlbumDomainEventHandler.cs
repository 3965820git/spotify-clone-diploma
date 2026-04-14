using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Events;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Albums;

internal sealed class TrackAddedToAlbumDomainEventHandler(
    ICatalogUnitOfWork unit,
    ILogger<TrackAddedToAlbumDomainEventHandler> logger)
    : INotificationHandler<TrackAddedToAlbumDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ILogger<TrackAddedToAlbumDomainEventHandler> _logger = logger;

    public async Task Handle(
        TrackAddedToAlbumDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
                notification.AlbumId, cancellationToken);
        if (album is null)
        {
            _logger.LogError(
                "Album {AlbumId} not found while attaching track {TrackId} to it",
                notification.AlbumId.Value,
                notification.TrackId.Value);
            return;
        }

        Track? track = await _unit.Tracks.GetByIdAsync(
                notification.TrackId, cancellationToken);
        if (track is null)
        {
            _logger.LogError(
                "Track {TrackId} not found while attaching it to Album {AlbumId}",
                notification.TrackId.Value,
                notification.AlbumId.Value);
            return;
        }

        track.MoveToAlbum(album.Id, album.Status.IsPublished);

        await _unit.CommitAsync(cancellationToken);
    }
}
