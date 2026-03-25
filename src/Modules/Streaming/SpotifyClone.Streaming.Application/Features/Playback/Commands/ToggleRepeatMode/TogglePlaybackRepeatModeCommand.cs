using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.ToggleRepeatMode;

public sealed record TogglePlaybackRepeatModeCommand(
    Guid DeviceId)
    : ICommand<TogglePlaybackRepeatModeCommandResult>;
