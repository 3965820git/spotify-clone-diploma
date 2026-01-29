using Microsoft.EntityFrameworkCore;
using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Auth.Repositories;

internal sealed class RefreshTokenEfCoreRepository(AccountsAppDbContext context) : IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _refreshTokens = context.RefreshTokens;

    public async Task<Result<RefreshTokenEnvelope>> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default)
    {
        RefreshToken? refreshToken = await _refreshTokens
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);

        if (refreshToken is null)
        {
            return Result.Failure<RefreshTokenEnvelope>(RefreshTokenErrors.NotFound);
        }

        return new RefreshTokenEnvelope(
            refreshToken.UserId,
            refreshToken.TokenHash,
            refreshToken.ExpiresAt,
            refreshToken.IsActive);
    }

    public async Task<Result> StoreAsync(
        UserId userId,
        string tokenHash,
        DateTimeOffset expiresAt,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
        {
            return Result.Failure(RefreshTokenErrors.InvalidToken);
        }

        var token = new RefreshToken(
            userId,
            tokenHash,
            expiresAt);

        await _refreshTokens.AddAsync(token, cancellationToken);

        return Result.Success();
    }

    public async Task<Result> RevokeAsync(
        string tokenHash,
        string? replacedByTokenHash,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
        {
            return Result.Failure(RefreshTokenErrors.InvalidToken);
        }

        RefreshToken? token = await _refreshTokens
            .FirstOrDefaultAsync(
                t => t.TokenHash == tokenHash,
                cancellationToken);

        if (token is null)
        {
            return Result.Success();
        }

        token.Revoke(replacedByTokenHash);

        return Result.Success();
    }

    public async Task<Result> RevokeAllAsync(
        UserId userId,
        string? replacedByTokenHash,
        CancellationToken cancellationToken = default)
    {
        List<RefreshToken> tokens = await _refreshTokens
            .Where(t => t.UserId == userId && t.RevokedAt == null)
            .ToListAsync(cancellationToken);

        foreach (RefreshToken token in tokens)
        {
            token.Revoke(replacedByTokenHash);
        }

        return Result.Success();
    }
}
