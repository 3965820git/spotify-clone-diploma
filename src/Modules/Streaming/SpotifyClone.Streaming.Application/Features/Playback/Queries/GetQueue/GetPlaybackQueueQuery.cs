using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Streaming.Application.Features.Playback.Queries.GetQueue;

public sealed record GetPlaybackQueueQuery
    : IQuery<PlaybackQueueDetails>;
