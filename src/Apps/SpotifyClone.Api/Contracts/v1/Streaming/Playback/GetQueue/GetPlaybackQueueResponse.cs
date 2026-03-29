using SpotifyClone.Catalog.Application.Features.Tracks.Queries;

namespace SpotifyClone.Api.Contracts.v1.Streaming.Playback.GetQueue;

public sealed record GetPlaybackQueueResponse(
    TrackSummary? CurrentTrack,
    IEnumerable<TrackSummary> TracksInQueue);
