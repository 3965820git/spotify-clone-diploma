namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface IOtpCacheService
{
    Task SaveOtpAsync(
        string phoneNumber,
        string code,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    Task<string?> GetOtpAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);

    Task RemoveOtpAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);
}
