using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Streaming.Application.Features.Playback.Queries.GetDetails;

public sealed record GetPlaybackSessionDetailsQuery
    : IQuery<PlaybackSessionDetails>;
