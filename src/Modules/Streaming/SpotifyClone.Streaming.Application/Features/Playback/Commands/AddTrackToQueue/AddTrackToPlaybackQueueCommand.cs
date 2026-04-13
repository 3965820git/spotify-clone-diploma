using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.AddTrackToQueue;

public sealed record AddTrackToPlaybackQueueCommand(
    Guid TrackId)
    : ICommand<AddTrackToPlaybackQueueCommandResult>;
