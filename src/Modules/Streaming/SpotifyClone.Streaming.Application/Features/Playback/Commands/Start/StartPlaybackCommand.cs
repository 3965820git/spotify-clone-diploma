using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;

public sealed record StartPlaybackCommand(
    Guid DeviceId,
    string ContextType,
    Guid? ContextExternalId,
    IEnumerable<Guid> TrackIds)
    : ICommand<StartPlaybackCommandResult>;
