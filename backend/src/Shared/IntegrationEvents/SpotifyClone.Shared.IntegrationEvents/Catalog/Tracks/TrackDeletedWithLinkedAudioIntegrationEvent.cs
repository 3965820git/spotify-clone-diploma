using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

public sealed record TrackDeletedWithLinkedAudioIntegrationEvent(
    Guid TrackId,
    Guid AudioId)
    : IntegrationEvent;
