using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotifyClone.Billing.Application.Abstractions;
using SpotifyClone.Billing.Application.Abstractions.Services;
using SpotifyClone.Billing.Application.Errors;
using SpotifyClone.Billing.Application.Models;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Configuration;
using SpotifyClone.Shared.BuildingBlocks.Application.Email;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.HandleCheckoutWebhook;

internal sealed class HandleCheckoutWebhookCommandHandler(
    IBillingUnitOfWork unit,
    IEmailSender emailSender,
    IPaymentProviderService paymentProviderService,
    IOptions<ApplicationSettings> appSettings,
    ILogger<HandleCheckoutWebhookCommandHandler> logger)
    : ICommandHandler<HandleCheckoutWebhookCommand, HandleCheckoutWebhookCommandResult>
{
    private readonly IBillingUnitOfWork _unit = unit;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IPaymentProviderService _paymentProviderService = paymentProviderService;
    private readonly ApplicationSettings _appSettings = appSettings.Value;
    private readonly ILogger<HandleCheckoutWebhookCommandHandler> _logger = logger;

    public async Task<Result<HandleCheckoutWebhookCommandResult>> Handle(
        HandleCheckoutWebhookCommand request,
        CancellationToken cancellationToken)
        => request.EventType switch
        {
            CheckoutWebhookEvents.CheckoutSessionCompleted => await HandleCheckoutCompletedAsync(
                request, cancellationToken),
            CheckoutWebhookEvents.InvoicePaid => await HandleInvoicePaidAsync(
                request, cancellationToken),
            CheckoutWebhookEvents.SubscriptionDeleted => await HandleSubscriptionDeletedAsync(
                request, cancellationToken),
            CheckoutWebhookEvents.InvoicePaymentFailed => await HandleInvoicePaymentFailedAsync(
                request, cancellationToken),
            _ => new HandleCheckoutWebhookCommandResult(),
        };

    private async Task<Result<HandleCheckoutWebhookCommandResult>> HandleInvoicePaymentFailedAsync(
        HandleCheckoutWebhookCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.SubscriptionId))
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.InvalidWebhookData);
        }

        Subscription? subscription = await _unit.Subscriptions.GetByExternalIdAsync(
            request.SubscriptionId, cancellationToken);
        if (subscription is null)
        {
            _logger.LogWarning(
                "Payment failed webhook received for unknown subscription: {SubId}",
                request.SubscriptionId);
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.NotFound);
        }

        string customerEmail = await _paymentProviderService.GetCustomerEmailAsync(
            request.CustomerId!, cancellationToken);

        var message = new EmailMessage(
            To: [customerEmail],
            Subject: $"Проблема з оплатою Premium - {_appSettings.DomainName}",
            HtmlBody: $@"
                <h1>Ой! Виникла проблема з оплатою</h1>
                <p>Ми не змогли списати кошти за вашу підписку Premium.</p>
                <p>Будь ласка, перевірте баланс вашої картки, щоб не втратити доступ до музики без реклами.</p>
                <p>Ми спробуємо повторити оплату автоматично протягом найближчого часу.</p>",
            PlainTextBody: "Ми не змогли списати кошти за вашу підписку. Перевірте баланс картки.");

        Result emailResult = await _emailSender.SendAsync(message, cancellationToken: cancellationToken);
        if (emailResult.IsFailure)
        {
            _logger.LogError("Failed to send payment failure email to {Email}", customerEmail);
        }

        return new HandleCheckoutWebhookCommandResult();
    }

    private async Task<Result<HandleCheckoutWebhookCommandResult>> HandleCheckoutCompletedAsync(
        HandleCheckoutWebhookCommand request,
        CancellationToken cancellationToken)
    {
        if (request.UserId is null || request.SubscriptionId is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.InvalidWebhookData);
        }

        var subscription = Subscription.Create(
            SubscriptionId.New(),
            UserId.From(request.UserId.Value),
            request.CustomerId!);

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
        Subscription? subscription = await _unit.Subscriptions.GetByExternalIdAsync(
            request.SubscriptionId!, cancellationToken);

        if (subscription is null && request.UserId.HasValue)
        {
            subscription = await _unit.Subscriptions.GetByUserIdAsync(
                UserId.From(request.UserId.Value), cancellationToken);
        }

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
        Subscription? subscription = await _unit.Subscriptions.GetByExternalIdAsync(
            request.SubscriptionId!, cancellationToken);
        if (subscription is null)
        {
            return Result.Failure<HandleCheckoutWebhookCommandResult>(SubscriptionErrors.NotFound);
        }

        subscription.Expire();

        return new HandleCheckoutWebhookCommandResult();
    }
}
