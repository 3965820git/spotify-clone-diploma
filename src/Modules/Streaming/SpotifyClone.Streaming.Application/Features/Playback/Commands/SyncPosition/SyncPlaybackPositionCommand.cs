using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SyncPosition;

public sealed record SyncPlaybackPositionCommand(
    Guid DeviceId,
    int PositionMs)
    : ICommand<SyncPlaybackPositionCommandResult>;
