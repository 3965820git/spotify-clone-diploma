using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.Events;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Genres;

internal sealed class GenreDeletedDomainEventHandler(
    ICatalogUnitOfWork unit)
    : INotificationHandler<GenreDeletedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task Handle(
        GenreDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        IEnumerable<Track> tracks = await _unit.Tracks.GetAllByGenreAsync(
            notification.GenreId, cancellationToken);

        foreach (Track track in tracks)
        {
            track.RemoveGenre(notification.GenreId);
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
