using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Billing.Application.Errors;

public sealed class BillingDomainExceptionMapper : IDomainExceptionMapper
{
    public Error MapToError(DomainExceptionBase domainException)
        => domainException switch
        {
            // Subscriptions
            InvalidSubscriptionPeriodDomainException => SubscriptionErrors.InvalidPeriod,
            InvalidSubscriptionStateDomainException => SubscriptionErrors.InvalidState,

            // Other
            _ => CommonErrors.Unknown
        };
}
