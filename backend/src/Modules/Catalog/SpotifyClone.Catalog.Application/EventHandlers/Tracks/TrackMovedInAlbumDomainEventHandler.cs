using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Events;
using SpotifyClone.Catalog.Domain.Services;

namespace SpotifyClone.Catalog.Application.EventHandlers.Tracks;

internal sealed class TrackMovedInAlbumDomainEventHandler(
    ICatalogUnitOfWork unit,
    AlbumTrackDomainService albumTrackDomainService,
    ILogger<TrackMovedInAlbumDomainEventHandler> logger)
    : INotificationHandler<TrackMovedInAlbumDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;
    private readonly ILogger<TrackMovedInAlbumDomainEventHandler> _logger = logger;

    public async Task Handle(
        TrackMovedInAlbumDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(notification.NewAlbumId, cancellationToken);

        if (album is null)
        {
            _logger.LogError(
                "Album {AlbumId} not found while moving Track {TrackId} from a different album",
                notification.NewAlbumId.Value,
                notification.TrackId.Value);

            return;
        }

        album.AddTrack(notification.TrackId);
        _albumTrackDomainService.TryMarkAlbumAsReadyToPublish(album);
        _albumTrackDomainService.ReevaluateAlbumType(album);

        await _unit.CommitAsync(cancellationToken);
    }
}
