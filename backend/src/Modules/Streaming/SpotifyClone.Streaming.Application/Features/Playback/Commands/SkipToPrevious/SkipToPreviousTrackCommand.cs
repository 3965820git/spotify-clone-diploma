using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToPrevious;

public sealed record SkipToPreviousTrackCommand(
    Guid DeviceId)
    : ICommand<SkipToPreviousTrackCommandResult>;
