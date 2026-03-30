namespace SpotifyClone.Billing.Application.Features.Subscriptions.Queries;

public sealed record SubscriptionDetails(
    bool IsPremium,
    string? Status,
    DateTimeOffset? CurrentPeriodEnd,
    bool? WillCancelAtPeriodEnd);
