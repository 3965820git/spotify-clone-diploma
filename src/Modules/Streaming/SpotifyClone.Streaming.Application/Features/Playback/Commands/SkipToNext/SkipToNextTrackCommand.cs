using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;

public sealed record SkipToNextTrackCommand(
    Guid DeviceId)
    : ICommand<SkipToNextTrackCommandResult>;
