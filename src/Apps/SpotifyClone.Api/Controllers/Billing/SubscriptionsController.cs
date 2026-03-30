using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SpotifyClone.Api.Contracts.v1.Billing.Subscriptions;
using SpotifyClone.Api.Mappers;
using SpotifyClone.Billing.Application.Features.Subscriptions.Commands.CreateCheckoutSession;
using SpotifyClone.Billing.Application.Features.Subscriptions.Commands.HandleCheckoutWebhook;
using SpotifyClone.Billing.Application.Models;
using SpotifyClone.Billing.Infrastructure.Payment;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using Stripe;
using Stripe.Checkout;

namespace SpotifyClone.Api.Controllers.Billing;

[Route("api/v1/billing")]
public sealed class BillingController(IMediator mediator)
    : ApiController(mediator)
{
    [EndpointSummary("Create Checkout session")]
    [EndpointDescription("Create a Checkout session for the current user.")]
    [ProducesResponseType(typeof(CreateCheckoutSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = UserRoles.Listener)]
    [HttpPost("checkout")]
    public async Task<ActionResult<CreateCheckoutSessionResponse>> CreateCheckoutSession(
        CancellationToken cancellationToken = default)
    {
        Result<CreateCheckoutSessionCommandResult> result = await Mediator.Send(
            new CreateCheckoutSessionCommand(),
            cancellationToken);
        if (result.IsFailure)
        {
            ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                result, HttpContext);

            return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        }

        return Ok(new CreateCheckoutSessionResponse(result.Value.CheckoutUrl));
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> HandleWebhook(
        [FromHeader(Name = "Stripe-Signature")] string signature,
        IOptions<StripeSettings> stripeSettings,
        ILogger<CreateCheckoutSessionCommand> logger,
        CancellationToken cancellationToken = default)
    {
        string json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(cancellationToken);
        string endpointSecret = stripeSettings.Value.WebhookSecret;

        try
        {
            // 1. Криптографічна перевірка підпису (найважливіший крок безпеки!)
            Event stripeEvent = EventUtility.ConstructEvent(json, signature, endpointSecret);

            // 2. Розбираємо подію і мапимо на нашу команду
            HandleCheckoutWebhookCommand? command = null;

            if (stripeEvent.Type == CheckoutWebhookEvents.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;

                Guid userId = Guid.Empty;
                if (session?.Metadata != null && session.Metadata.TryGetValue("UserId", out string? userIdString))
                {
                    Guid.TryParse(userIdString, out userId);
                }

                if (userId == Guid.Empty)
                {
                    logger.LogWarning("Received webhook without UserId. SessionId: {SessionId}", session?.Id);
                    return Ok();
                }

                command = new HandleCheckoutWebhookCommand(
                    stripeEvent.Type,
                    session!.CustomerId,
                    session.SubscriptionId,
                    userId == Guid.Empty ? null : userId,
                    null, null
                );
            }
            else if (stripeEvent.Type == CheckoutWebhookEvents.InvoicePaid)
            {
                var invoice = stripeEvent.Data.Object as Invoice;
                InvoiceLineItem? lineItem = invoice?.Lines?.Data?.FirstOrDefault();

                command = new HandleCheckoutWebhookCommand(
                    stripeEvent.Type,
                    invoice!.CustomerId,
                    lineItem?.SubscriptionId,
                    null,
                    lineItem?.Period?.Start,
                    lineItem?.Period?.End
                );
            }
            else if (stripeEvent.Type == CheckoutWebhookEvents.SubscriptionDeleted)
            {
                var subscription = stripeEvent.Data.Object as Subscription;
                command = new HandleCheckoutWebhookCommand(
                    stripeEvent.Type,
                    subscription!.CustomerId,
                    subscription.Id,
                    null, null, null
                );
            }

            // 3. Відправляємо команду в Application шар
            if (command is not null)
            {
                Result<HandleCheckoutWebhookCommandResult> result = await Mediator.Send(
                    command, cancellationToken);
                if (result.IsFailure)
                {
                    ProblemDetails problemDetails = ResultToProblemDetailsMapper.MapToProblemDetails(
                        result, HttpContext);

                    return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
                }
            }

            // Stripe вимагає 200 OK, інакше він буде повторювати запит знову і знову (Retry)
            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest($"Webhook Error: {e.Message}");
        }
    }
}
