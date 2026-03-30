using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Enums;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Events;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Exceptions;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Domain.Aggregates.Subscriptions;

public sealed class Subscription : AggregateRoot<SubscriptionId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public PaymentProviderIdentity ExternalIdentity { get; private set; } = null!;
    public SubscriptionStatus Status { get; private set; }
    public DateTimeOffset? CurrentPeriodStart { get; private set; }
    public DateTimeOffset? CurrentPeriodEnd { get; private set; }
    public bool CancelAtPeriodEnd { get; private set; }

    public static Subscription Create(
        SubscriptionId id,
        UserId userId,
        string paymentProviderCustomerId)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentException.ThrowIfNullOrWhiteSpace(paymentProviderCustomerId);

        var subscription = new Subscription(
            id, userId,
            new PaymentProviderIdentity(paymentProviderCustomerId, null),
            SubscriptionStatus.Pending,
            null, null, false);

        subscription.RaiseDomainEvent(new SubscriptionCreatedDomainEvent(subscription.Id));
        return subscription;
    }

    public void Activate(
        string externalSubscriptionId,
        DateTimeOffset periodStart,
        DateTimeOffset periodEnd)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(externalSubscriptionId);

        if (Status == SubscriptionStatus.Active)
        {
            return;
        }

        if (periodEnd <= periodStart)
        {
            throw new InvalidSubscriptionPeriodDomainException(
                "End date must be strictly after start date.");
        }

        if (Status == SubscriptionStatus.Canceled || Status == SubscriptionStatus.Expired)
        {
            throw new InvalidSubscriptionStateDomainException(
                $"Cannot activate subscription in {Status} state.");
        }

        ExternalIdentity = new PaymentProviderIdentity(ExternalIdentity.CustomerId, externalSubscriptionId);
        Status = SubscriptionStatus.Active;
        CurrentPeriodStart = periodStart.ToUniversalTime();
        CurrentPeriodEnd = periodEnd.ToUniversalTime();
        CancelAtPeriodEnd = false;

        RaiseDomainEvent(new SubscriptionActivatedDomainEvent(Id, UserId));
    }

    public void Renew(
        DateTimeOffset newPeriodStart,
        DateTimeOffset newPeriodEnd)
    {
        if (Status != SubscriptionStatus.Active && Status != SubscriptionStatus.PastDue)
        {
            throw new InvalidSubscriptionStateDomainException(
                $"Cannot renew subscription in {Status} state.");
        }

        if (newPeriodEnd <= newPeriodStart)
        {
            throw new InvalidSubscriptionPeriodDomainException(
                "End date must be strictly after start date.");
        }

        Status = SubscriptionStatus.Active;
        CurrentPeriodStart = newPeriodStart.ToUniversalTime();
        CurrentPeriodEnd = newPeriodEnd.ToUniversalTime();
        CancelAtPeriodEnd = false;

        RaiseDomainEvent(new SubscriptionRenewedDomainEvent(Id, UserId));
    }

    public void Cancel()
    {
        if (Status != SubscriptionStatus.Active)
        {
            throw new InvalidSubscriptionStateDomainException(
                "Only active subscriptions can be canceled.");
        }

        CancelAtPeriodEnd = true;
        Status = SubscriptionStatus.Canceled;

        RaiseDomainEvent(new SubscriptionCanceledDomainEvent(Id, UserId));
    }

    public void Expire()
    {
        if (CurrentPeriodEnd > DateTimeOffset.UtcNow)
        {
            throw new InvalidSubscriptionStateDomainException(
                "Cannot expire subscription before its period ends.");
        }

        Status = SubscriptionStatus.Expired;

        RaiseDomainEvent(new SubscriptionExpiredDomainEvent(Id, UserId));
    }

    private Subscription(
        SubscriptionId id, UserId userId, PaymentProviderIdentity externalIdentity, SubscriptionStatus status,
        DateTimeOffset? currentPeriodStart, DateTimeOffset? currentPeriodEnd, bool cancelAtPeriodEnd)
        : base(id)
    {
        UserId = userId;
        ExternalIdentity = externalIdentity;
        Status = status;
        CurrentPeriodStart = currentPeriodStart;
        CurrentPeriodEnd = currentPeriodEnd;
        CancelAtPeriodEnd = cancelAtPeriodEnd;
    }

    private Subscription()
    {
    }
}
