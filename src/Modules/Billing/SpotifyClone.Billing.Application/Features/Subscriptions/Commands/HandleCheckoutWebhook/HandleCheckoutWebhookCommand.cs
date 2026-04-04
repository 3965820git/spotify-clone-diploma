using SpotifyClone.Billing.Application.Abstractions;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.HandleCheckoutWebhook;

public record HandleCheckoutWebhookCommand(
    string EventType,
    string CustomerId,
    string? SubscriptionId,
    Guid? UserId,
    DateTimeOffset? PeriodStart,
    DateTimeOffset? PeriodEnd)
    : IBillingPersistentCommand<HandleCheckoutWebhookCommandResult>;
