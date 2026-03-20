using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Repositories;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;

namespace SpotifyClone.Streaming.Application.Abstractions;

public interface IStreamingUnitOfWork : IUnitOfWork
{
    IAudioAssetRepository AudioAssets { get; }
    IImageAssetRepository ImageAssets { get; }
    IPlaybackSessionRepository PlaybackSessions { get; }
    IOutboxRepository OutboxMessages { get; }
}
