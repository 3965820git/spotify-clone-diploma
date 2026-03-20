using MediatR;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Repositories;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Infrastructure.Persistence.Database;

namespace SpotifyClone.Streaming.Infrastructure.Persistence;

internal sealed class StreamingEfCoreUnitOfWork(
    StreamingAppDbContext context,
    IAudioAssetRepository audioAssets,
    IImageAssetRepository imageAssets,
    IPlaybackSessionRepository playbackSessions,
    IOutboxRepository outbox,
    IPublisher publisher)
    : EfCoreUnitOfWorkBase<StreamingAppDbContext>(context, publisher),
    IStreamingUnitOfWork
{
    public IAudioAssetRepository AudioAssets => audioAssets;
    public IImageAssetRepository ImageAssets => imageAssets;
    public IPlaybackSessionRepository PlaybackSessions => playbackSessions;
    public IOutboxRepository OutboxMessages => outbox;
}
