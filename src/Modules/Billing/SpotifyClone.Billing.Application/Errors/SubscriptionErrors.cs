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

    public static readonly Error AlreadyActivated = new(
        "Subscription.AlreadyActivated",
        "The specified user is already have an activated subscription.");

    public static readonly Error InvalidWebhookData = new(
        "Subscription.InvalidWebhookData",
        "The provided webhook data is invalid.");

    public static readonly Error NotLoggedIn = new(
        "Subscription.NotLoggedIn",
        "The current user is not logged in.");

    public static readonly Error NotFound = CommonErrors.NotFound(
        "Subscription", "Subscription");
}
