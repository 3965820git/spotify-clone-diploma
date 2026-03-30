using SpotifyClone.Billing.Application.Abstractions;
using SpotifyClone.Billing.Application.Errors;
using SpotifyClone.Billing.Application.Models;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.HandleCheckoutWebhook;

internal sealed class HandleCheckoutWebhookCommandHandler(
    IBillingUnitOfWork unit)
    : ICommandHandler<HandleCheckoutWebhookCommand, HandleCheckoutWebhookCommandResult>
{
    private readonly IBillingUnitOfWork _unit = unit;

    public async Task<Result<HandleCheckoutWebhookCommandResult>> Handle(
        HandleCheckoutWebhookCommand request,
        CancellationToken cancellationToken)
    {
        if (request.UserId is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.InvalidWebhookData);
        }

        return request.EventType switch
        {
            CheckoutWebhookEvents.CheckoutSessionCompleted => await HandleCheckoutCompletedAsync(
                request, cancellationToken),
            CheckoutWebhookEvents.InvoicePaid => await HandleInvoicePaidAsync(
                request, cancellationToken),
            CheckoutWebhookEvents.SubscriptionDeleted => await HandleSubscriptionDeletedAsync(
                request, cancellationToken),
            _ => new HandleCheckoutWebhookCommandResult(),// Ігноруємо інші події
        };
    }

    private async Task<Result<HandleCheckoutWebhookCommandResult>> HandleCheckoutCompletedAsync(
        HandleCheckoutWebhookCommand request,
        CancellationToken cancellationToken)
    {
        if (request.SubscriptionId is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.InvalidWebhookData);
        }
        var subscription = Subscription.Create(
            SubscriptionId.New(),
            UserId.From(request.UserId!.Value),
            request.CustomerId);

        subscription.Activate(
            request.SubscriptionId,
            request.PeriodStart ?? DateTimeOffset.UtcNow,
            request.PeriodEnd ?? DateTimeOffset.UtcNow.AddDays(30));

        await _unit.Subscriptions.AddAsync(subscription, cancellationToken);

        return new HandleCheckoutWebhookCommandResult();
    }

    private async Task<Result<HandleCheckoutWebhookCommandResult>> HandleInvoicePaidAsync(
        HandleCheckoutWebhookCommand request,
        CancellationToken cancellationToken)
    {
        if (request.SubscriptionId is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.InvalidWebhookData);
        }

        Subscription? subscription = await _unit.Subscriptions.GetByUserIdAsync(
            UserId.From(request.UserId!.Value), cancellationToken);
        if (subscription is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.NotFound);
        }

        if (request.PeriodStart.HasValue && request.PeriodEnd.HasValue)
        {
            subscription.Renew(request.PeriodStart.Value, request.PeriodEnd.Value);
        }

        return new HandleCheckoutWebhookCommandResult();
    }

    private async Task<Result<HandleCheckoutWebhookCommandResult>> HandleSubscriptionDeletedAsync(
        HandleCheckoutWebhookCommand request,
        CancellationToken cancellationToken)
    {
        if (request.SubscriptionId is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.InvalidWebhookData);
        }

        Subscription? subscription = await _unit.Subscriptions.GetByUserIdAsync(
            UserId.From(request.UserId!.Value), cancellationToken);

        if (subscription is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.NotFound);
        }

        subscription.Expire();

        return new HandleCheckoutWebhookCommandResult();
    }
}
