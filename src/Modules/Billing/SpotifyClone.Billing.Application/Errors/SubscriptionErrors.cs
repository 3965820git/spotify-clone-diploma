using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Billing.Application.Errors;

public static class SubscriptionErrors
{
    public static readonly Error InvalidPeriod = new(
        "Subscription.InvalidPeriod",
        "Subscription period is invalid.");

    public static readonly Error InvalidState = new(
        "Subscription.InvalidState",
        "Subscription state is invalid.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "Subscription", "Subscription");
}
