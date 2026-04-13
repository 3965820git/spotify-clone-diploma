using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.IntegrationEvents.Catalog.Albums;

public sealed record ImageLinkAddedIntegrationEvent(
    Guid ImageId)
    : IntegrationEvent;
