using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Streaming.Application.Features.Playback.Queries;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;

public sealed record StartPlaybackCommand(
    Guid TrackId,
    Guid DeviceId,
    string ContextType,
    Guid? ContextExternalId,
    int? PositionMs)
    : ICommand<PlaybackSessionDetails>;
