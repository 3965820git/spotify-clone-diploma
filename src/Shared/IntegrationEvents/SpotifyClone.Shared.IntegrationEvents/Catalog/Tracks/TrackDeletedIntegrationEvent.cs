using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.IntegrationEvents.Catalog.Tracks;

public sealed record TrackDeletedIntegrationEvent(
    Guid TrackId)
    : IntegrationEvent;
