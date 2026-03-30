namespace SpotifyClone.Billing.Application.Features.Subscriptions.Queries;

public sealed record SubscriptionDetails(
    Guid Id,
    Guid UserId,
    string ExternalCustomerId,
    string? ExternalSubscriptionId,
    string Status,
    DateTimeOffset? CurrentPeriodStart,
    DateTimeOffset? CurrentPeriodEnd,
    bool CancelAtPeriodEnd);
