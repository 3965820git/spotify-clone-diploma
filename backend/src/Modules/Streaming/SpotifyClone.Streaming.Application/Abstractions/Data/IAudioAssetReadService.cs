using SpotifyClone.Shared.Kernel.IDs;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.ValueObjects;

namespace SpotifyClone.Streaming.Application.Abstractions.Data;

public interface IAudioAssetReadService
{
    Task<AudioAssetId?> GetByTrackId(
        TrackId trackId,
        CancellationToken cancellationToken = default);
}
