using MediatR;
using SpotifyClone.Billing.Application.Abstractions;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.Events;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;
using SpotifyClone.Shared.IntegrationEvents.Billing.Subscriptions;

namespace SpotifyClone.Billing.Application.EventHandlers.Subscriptions;

internal sealed class SubscriptionExpiredDomainEventHandler(
    IBillingUnitOfWork unit)
    : INotificationHandler<SubscriptionExpiredDomainEvent>
{
    private readonly IBillingUnitOfWork _unit = unit;

    public async Task Handle(
        SubscriptionExpiredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new UserSubscriptionStatusChangedIntegrationEvent(
            notification.UserId.Value, false);

        var message = OutboxMessage.FromIntegrationEvent(integrationEvent);

        await _unit.OutboxMessages.AddAsync(message, cancellationToken);
        await _unit.CommitAsync(cancellationToken);
    }
}
