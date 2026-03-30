namespace SpotifyClone.Billing.Infrastructure.Payment;

public sealed record StripeSettings(
    string SecretKey,
    string PublishableKey,
    string PremiumPriceId,
    string WebhookSecret)
{
    public const string SectionName = "Stripe";
}
