using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Events;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Domain.Services;

namespace SpotifyClone.Catalog.Application.EventHandlers.Albums;

internal sealed class AlbumUnpublishedDomainEventHandler(
    ICatalogUnitOfWork unit,
    AlbumTrackDomainService albumTrackDomainService,
    ILogger<AlbumUnpublishedDomainEventHandler> logger)
    : INotificationHandler<AlbumUnpublishedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;
    private readonly ILogger<AlbumUnpublishedDomainEventHandler> _logger = logger;

    public async Task Handle(
        AlbumUnpublishedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(notification.AlbumId, cancellationToken);
        if (album is null)
        {
            _logger.LogError("Album {AlbumId} was not found.", notification.AlbumId);
            throw new InvalidOperationException($"Album {notification.AlbumId} was not found.");
        }

        IEnumerable<Track> tracks = await _unit.Tracks.GetAllByAlbumAsync(
            notification.AlbumId,
            cancellationToken);

        foreach (Track track in tracks)
        {
            track.Unpublish();
        }

        _albumTrackDomainService.TryMarkAlbumAsReadyToPublish(album);

        await _unit.CommitAsync(cancellationToken);
    }
}
