namespace SpotifyClone.Billing.Application.Abstractions.Services;

public interface ICheckoutService
{
    Task<string> CreateCheckoutSessionUrlAsync(
        Guid userId,
        string userEmail,
        CancellationToken cancellationToken = default);
}
