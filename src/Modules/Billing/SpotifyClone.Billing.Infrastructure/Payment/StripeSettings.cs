namespace SpotifyClone.Billing.Infrastructure.Payment;

public sealed record StripeSettings
{
    public const string SectionName = "Stripe";
    public required string SecretKey { get; init; }
    public required string PublishableKey { get; init; }
    public required string PremiumPriceId { get; init; }
    public required string WebhookSecret { get; init; }
}
