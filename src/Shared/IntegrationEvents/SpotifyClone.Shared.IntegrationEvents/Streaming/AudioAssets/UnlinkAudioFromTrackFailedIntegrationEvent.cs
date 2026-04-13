using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.IntegrationEvents.Streaming.AudioAssets;

public sealed record UnlinkAudioFromTrackFailedIntegrationEvent(
    Guid AudioId,
    Guid TrackId,
    TimeSpan Duration)
    : IntegrationEvent;
