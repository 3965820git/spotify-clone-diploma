namespace SpotifyClone.Api.Contracts.v1.Billing.Subscriptions;

public sealed record CreateCheckoutSessionResponse(
    string CheckoutUrl);
