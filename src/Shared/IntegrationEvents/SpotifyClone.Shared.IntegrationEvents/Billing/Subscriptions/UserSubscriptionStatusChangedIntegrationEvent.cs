using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.IntegrationEvents.Billing.Subscriptions;

public sealed record UserSubscriptionStatusChangedIntegrationEvent(
    Guid UserId,
    bool IsPremium)
    : IntegrationEvent;
