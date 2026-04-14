using SpotifyClone.Billing.Application.Abstractions;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.Cancel;

public sealed record CancelSubscriptionCommand
    : IBillingPersistentCommand<CancelSubscriptionCommandResult>;
