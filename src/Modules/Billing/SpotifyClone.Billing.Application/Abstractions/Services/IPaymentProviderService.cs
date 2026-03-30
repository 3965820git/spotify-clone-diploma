namespace SpotifyClone.Billing.Application.Abstractions.Services;

public interface IPaymentProviderService
{
    Task<string> CreateCheckoutSessionUrlAsync(
        Guid userId,
        string? email,
        CancellationToken cancellationToken = default);

    Task CancelSubscriptionAtPeriodEndAsync(
        string externalSubscriptionId,
        CancellationToken cancellationToken = default);
}
