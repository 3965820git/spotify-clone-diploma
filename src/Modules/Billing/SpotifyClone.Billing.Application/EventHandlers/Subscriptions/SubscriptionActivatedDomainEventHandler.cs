using MediatR;
using SpotifyClone.Billing.Application.Abstractions;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Billing.Subscriptions;

namespace SpotifyClone.Billing.Application.EventHandlers.Subscriptions;

internal sealed class SubscriptionActivatedDomainEventHandler(
    IBillingUnitOfWork unit)
    : INotificationHandler<SubscriptionActivatedDomainEvent>
{
    private readonly IBillingUnitOfWork _unit = unit;

    public async Task Handle(
        SubscriptionActivatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new UserSubscriptionStatusChangedIntegrationEvent(
            notification.UserId.Value, true);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
