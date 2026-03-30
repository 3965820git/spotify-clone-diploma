using Microsoft.Extensions.Options;
using SpotifyClone.Billing.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Configuration;
using Stripe;
using Stripe.Checkout;

namespace SpotifyClone.Billing.Infrastructure.Payment;

internal sealed class StripeCheckoutService(
    IOptions<StripeSettings> stripeSettings,
    IOptions<ApplicationSettings> appSettings)
    : ICheckoutService
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;
    private readonly ApplicationSettings _appSettings = appSettings.Value;

    public async Task<string> CreateCheckoutSessionUrlAsync(
        Guid userId,
        string userEmail,
        CancellationToken cancellationToken = default)
    {
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

        var options = new SessionCreateOptions
        {
            CustomerEmail = userEmail,
            Mode = "subscription",

            LineItems =
            [
                new SessionLineItemOptions
                {
                    Price = _stripeSettings.PremiumPriceId,
                    Quantity = 1,
                }
            ],

            SubscriptionData = new SessionSubscriptionDataOptions
            {
                TrialPeriodDays = 30,
                // Метадані потрібні, щоб коли прийде Webhook, ми знали, якому юзеру видати Преміум
                Metadata = new Dictionary<string, string>
                {
                    { "UserId", userId.ToString() }
                }
            },

            SuccessUrl = $"{_appSettings.FrontendUrl}/premium/success",
            CancelUrl = $"{_appSettings.FrontendUrl}/premium/cancel",
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options, cancellationToken: cancellationToken);

        return session.Url;
    }
}
