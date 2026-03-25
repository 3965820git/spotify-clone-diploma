using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.RemoveTrackFromQueue;

public sealed record RemoveTrackFromPlaybackQueueCommand(
    Guid TrackId)
    : ICommand<RemoveTrackFromPlaybackQueueCommandResult>;
