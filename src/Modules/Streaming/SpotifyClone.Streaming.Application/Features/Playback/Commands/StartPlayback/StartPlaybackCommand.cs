using SpotifyClone.Streaming.Application.Abstractions;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.StartPlayback;

public sealed record StartPlaybackCommand(
    Guid TrackId,
    Guid DeviceId,
    string ContextType,
    Guid? ContextExternalId,
    int? PositionMs)
    : IStreamingPersistentCommand<StartPlaybackCommandResult>;
