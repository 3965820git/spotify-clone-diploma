using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Pause;

public sealed record PausePlaybackCommand(
    Guid DeviceId)
    : ICommand<PausePlaybackCommandResult>;
