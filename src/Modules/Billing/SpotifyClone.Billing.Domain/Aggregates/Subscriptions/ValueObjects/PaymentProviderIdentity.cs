using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;

public record PaymentProviderIdentity(
    string CustomerId,
    string? SubscriptionId)
    : ValueObject;
