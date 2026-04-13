using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries;

public interface IPlaybackHistoryEntryRepository
{
    Task AddAsync(
        PlaybackHistoryEntry playbackHistoryEntry,
        CancellationToken cancellationToken = default);

    Task GetById(
        PlaybackHistoryEntryId id,
        CancellationToken cancellationToken = default);
}
