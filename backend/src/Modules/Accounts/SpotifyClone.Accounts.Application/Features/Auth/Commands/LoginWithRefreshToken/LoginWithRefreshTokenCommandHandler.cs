using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithRefreshToken;

internal sealed class LoginWithRefreshTokenCommandHandler(
    IAccountsUnitOfWork unit,
    ITokenService tokenService,
    ITokenHasher tokenHasher,
    IIdentityService identity)
    : ICommandHandler<LoginWithRefreshTokenCommand, LoginWithRefreshTokenCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ITokenHasher _tokenHasher = tokenHasher;
    private readonly IIdentityService _identity = identity;

    public async Task<Result<LoginWithRefreshTokenCommandResult>> Handle(
        LoginWithRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        string oldRefreshTokenHash = _tokenHasher.Hash(request.RawToken);

        Result<RefreshTokenEnvelope> refreshTokenResult =
            await _unit.RefreshTokens.GetByTokenHashAsync(oldRefreshTokenHash, cancellationToken);

        if (refreshTokenResult.IsFailure)
        {
            return Result.Failure<LoginWithRefreshTokenCommandResult>(refreshTokenResult.Errors);
        }

        RefreshTokenEnvelope refreshToken = refreshTokenResult.Value;

        if (!refreshToken.IsActive)
        {
            Result revokeWithoutNewTokenResult
                = await _unit.RefreshTokens.RevokeAsync(
                    refreshToken.RawToken, null, cancellationToken);

            if (revokeWithoutNewTokenResult.IsFailure)
            {
                return Result.Failure<LoginWithRefreshTokenCommandResult>(refreshTokenResult.Errors);
            }

            return Result.Failure<LoginWithRefreshTokenCommandResult>(RefreshTokenErrors.Expired);
        }

        Result<IdentityUserInfo> identityResult =
            await _identity.GetUserInfoAsync(refreshToken.UserId, cancellationToken);

        if (identityResult.IsFailure)
        {
            return Result.Failure<LoginWithRefreshTokenCommandResult>(identityResult.Errors);
        }

        IdentityUserInfo user = identityResult.Value;
        AccessToken accessToken = _tokenService.GenerateAccessToken(user, [ "User" ]);

        RefreshTokenEnvelope newRefreshToken = _tokenService.GenerateRefreshToken(user.UserId);
        string newTokenHash = _tokenHasher.Hash(newRefreshToken.RawToken);

        Result revokeWithNewTokenResult = await _unit.RefreshTokens.RevokeAsync(
            oldRefreshTokenHash,
            newTokenHash,
            cancellationToken);

        if (revokeWithNewTokenResult.IsFailure)
        {
            return Result.Failure<LoginWithRefreshTokenCommandResult>(revokeWithNewTokenResult.Errors);
        }

        Result storeResult = await _unit.RefreshTokens.StoreAsync(
            user.UserId,
            newTokenHash,
            newRefreshToken.ExpiresAt,
            cancellationToken);

        if (storeResult.IsFailure)
        {
            return Result.Failure<LoginWithRefreshTokenCommandResult>(storeResult.Errors);
        }

        return Result.Success(new LoginWithRefreshTokenCommandResult(
            accessToken.RawToken,
            newRefreshToken.RawToken));
    }
}
