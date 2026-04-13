using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Events;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Albums;

internal sealed class AlbumDeletedDomainEventHandler(
    ICatalogUnitOfWork unit)
    : INotificationHandler<AlbumDeletedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task Handle(
        AlbumDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        IEnumerable<Track> tracks = await _unit.Tracks.GetAllByAlbumAsync(
            notification.AlbumId, cancellationToken);

        foreach (Track track in tracks)
        {
            track.Archive();
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
