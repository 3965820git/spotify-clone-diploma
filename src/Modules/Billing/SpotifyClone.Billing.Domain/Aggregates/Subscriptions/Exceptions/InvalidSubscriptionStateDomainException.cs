using SpotifyClone.Billing.Domain.Exceptions;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Exceptions;

public sealed class InvalidSubscriptionStateDomainException(string message)
    : BillingDomainExceptionBase(message);
