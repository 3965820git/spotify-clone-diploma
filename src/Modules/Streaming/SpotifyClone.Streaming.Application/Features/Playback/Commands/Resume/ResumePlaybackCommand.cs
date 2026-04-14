using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Resume;

public sealed record ResumePlaybackCommand(
    Guid DeviceId)
    : ICommand<ResumePlaybackCommandResult>;
