using SpotifyClone.Billing.Domain.Exceptions;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Exceptions;

public sealed class InvalidSubscriptionPeriodDomainException(string message)
    : BillingDomainExceptionBase(message);
