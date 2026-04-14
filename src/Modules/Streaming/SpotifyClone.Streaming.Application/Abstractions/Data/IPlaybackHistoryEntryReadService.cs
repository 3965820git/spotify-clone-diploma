using SpotifyClone.Streaming.Application.Features.Playback.Queries;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackHistoryEntries.ValueObjects;

namespace SpotifyClone.Streaming.Application.Abstractions.Data;

public interface IPlaybackHistoryEntryReadService
{
    Task<PlaybackHistoryEntryDetails?> GetDetailsAsync(
        PlaybackHistoryEntryId id,
        CancellationToken cancellationToken = default);
}
