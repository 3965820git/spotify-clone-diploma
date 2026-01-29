using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task<Result<RefreshTokenEnvelope>> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default);

    Task<Result> StoreAsync(
        UserId userId,
        string tokenHash,
        DateTimeOffset expiresAt,
        CancellationToken cancellationToken = default);

    Task<Result> RevokeAsync(
        string tokenHash,
        string? replacedByTokenHash,
        CancellationToken cancellationToken = default);

    Task<Result> RevokeAllAsync(
        UserId userId,
        string? replacedByTokenHash,
        CancellationToken cancellationToken = default);
}
