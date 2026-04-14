using Microsoft.Extensions.Caching.Distributed;
using SpotifyClone.Accounts.Application.Abstractions.Services;

namespace SpotifyClone.Accounts.Infrastructure.Services;

public sealed class OtpCacheService(
    IDistributedCache cache)
    : IOtpCacheService
{
    private readonly IDistributedCache _cache = cache;
    private const string KeyPrefix = "otp:";

    public async Task SaveOtpAsync(
        string phoneNumber,
        string code,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        string key = GetKey(phoneNumber);

        await _cache.SetStringAsync(key, code, options, cancellationToken);
    }

    public async Task<string?> GetOtpAsync(string phoneNumber, CancellationToken cancellationToken = default)
        => await _cache.GetStringAsync(GetKey(phoneNumber), cancellationToken);

    public async Task RemoveOtpAsync(string phoneNumber, CancellationToken cancellationToken = default)
        => await _cache.RemoveAsync(GetKey(phoneNumber), cancellationToken);

    private static string GetKey(string phoneNumber) => $"{KeyPrefix}{phoneNumber}";
}
