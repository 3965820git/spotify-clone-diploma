using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Billing.Domain.Exceptions;

public abstract class BillingDomainExceptionBase : DomainExceptionBase
{
    protected BillingDomainExceptionBase(string message)
        : base(message)
    {
    }
}
