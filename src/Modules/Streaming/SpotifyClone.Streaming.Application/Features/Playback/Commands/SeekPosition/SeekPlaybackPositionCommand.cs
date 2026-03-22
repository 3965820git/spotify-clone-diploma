using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SeekPosition;

public sealed record SeekPlaybackPositionCommand(
    Guid DeviceId,
    int PositionMs)
    : ICommand<SeekPlaybackPositionCommandResult>;
