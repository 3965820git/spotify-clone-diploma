using MediatR;
using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.Events;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;

namespace SpotifyClone.Catalog.Application.EventHandlers.Moods;

internal sealed class MoodDeletedDomainEventHandler(
    ICatalogUnitOfWork unit)
    : INotificationHandler<MoodDeletedDomainEvent>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task Handle(
        MoodDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        IEnumerable<Track> tracks = await _unit.Tracks.GetAllByMoodAsync(
            notification.MoodId, cancellationToken);

        foreach (Track track in tracks)
        {
            track.RemoveMood(notification.MoodId);
        }

        await _unit.CommitAsync(cancellationToken);
    }
}
