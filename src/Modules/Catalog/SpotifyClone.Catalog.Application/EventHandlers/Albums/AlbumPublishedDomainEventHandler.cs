using MediatR;
using Microsoft.Extensions.Logging;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Events;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Albums;

internal sealed class AlbumPublishedDomainEventHandler(
    ICatalogUnitOfWork unit,
    ILogger<AlbumPublishedDomainEventHandler> logger)
    : INotificationHandler<AlbumPublishedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ILogger<AlbumPublishedDomainEventHandler> _logger = logger;

    public async Task Handle(
        AlbumPublishedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(notification.AlbumId, cancellationToken);
        if (album is null)
        {
            _logger.LogError(
                "Album {Id} was not found while publishing it's tracks.",
                notification.AlbumId);
            return;
        }

        IEnumerable<Track> tracks = await _unit.Tracks.GetByIdsAsync(
            notification.Tracks, cancellationToken);

        foreach (Track track in tracks)
        {
            track.Publish(notification.ReleaseDate);
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
