using FluentValidation;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.HandleCheckoutWebhook;

public sealed class HandleCheckoutWebhookCommandValidator
    : AbstractValidator<HandleCheckoutWebhookCommand>
{
    public HandleCheckoutWebhookCommandValidator()
    {
        RuleFor(x => x.EventType)
            .NotEmpty().WithMessage("Event type is required.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");
    }
}
