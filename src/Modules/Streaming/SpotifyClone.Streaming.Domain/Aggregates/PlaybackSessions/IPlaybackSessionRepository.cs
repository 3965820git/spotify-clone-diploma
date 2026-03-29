using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;

public interface IPlaybackSessionRepository
{
    Task<PlaybackSession?> GetByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default);

    Task SaveAsync(
        PlaybackSession session,
        CancellationToken cancellationToken = default);

    Task DeleteSessionAsync(
        UserId userId,
        CancellationToken cancellationToken = default);

    Task DeleteQueueAsync(
        UserId userId,
        CancellationToken cancellationToken = default);
}
