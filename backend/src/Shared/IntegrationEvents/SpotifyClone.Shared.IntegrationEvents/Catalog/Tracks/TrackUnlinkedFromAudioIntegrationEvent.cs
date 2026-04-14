using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

public sealed record TrackUnlinkedFromAudioIntegrationEvent(
    Guid AudioId)
    : IntegrationEvent;
