using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;

namespace SpotifyClone.Streaming.Application.Abstractions.Data;

public interface IPlaybackSessionReadService
{
    Task<PlaybackSessionDetails?> GetDetails(
        UserId userId,
        CancellationToken cancellationToken = default);
}
