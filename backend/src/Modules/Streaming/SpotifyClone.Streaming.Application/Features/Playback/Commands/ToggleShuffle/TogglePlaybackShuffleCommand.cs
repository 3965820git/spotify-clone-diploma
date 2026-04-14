using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.ToggleShuffle;

public sealed record TogglePlaybackShuffleCommand(
    Guid DeviceId)
    : ICommand<TogglePlaybackShuffleCommandResult>;
