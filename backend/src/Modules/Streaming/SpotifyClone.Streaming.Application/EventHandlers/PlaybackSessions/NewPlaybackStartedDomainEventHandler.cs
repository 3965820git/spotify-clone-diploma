using MediatR;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Events;

namespace SpotifyClone.Streaming.Application.EventHandlers.PlaybackSessions;

internal sealed class NewPlaybackStartedDomainEventHandler(
    IStreamingUnitOfWork unit)
    : INotificationHandler<NewPlaybackStartedDomainEvent>
{
    private readonly IStreamingUnitOfWork _unit = unit;

    public async Task Handle(
        NewPlaybackStartedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var playbackHistoryEntry = PlaybackHistoryEntry.Create(
            PlaybackHistoryEntryId.New(),
            notification.UserId,
            notification.TrackId,
            notification.Context,
            notification.StartedAtUtc);

        await _unit.PlaybackHistoryEntries.AddAsync(playbackHistoryEntry, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
