using System.Security.Claims;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.RefreshToken;

internal sealed class LoginUserWithRefreshTokenCommandHandler(
    IAccountsUnitOfWork unit,
    ITokenService tokenService,
    ITokenHasher tokenHasher,
    IIdentityService identity)
    : ICommandHandler<LoginUserWithRefreshTokenCommand, LoginUserCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ITokenHasher _tokenHasher = tokenHasher;
    private readonly IIdentityService _identity = identity;

    public async Task<Result<LoginUserCommandResult>> Handle(
        LoginUserWithRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        string oldRefreshTokenHash = _tokenHasher.Hash(request.RawToken);

        Result<RefreshTokenEnvelope> refreshTokenResult =
            await _unit.RefreshTokens.GetByTokenHashAsync(oldRefreshTokenHash, cancellationToken);

        if (refreshTokenResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(refreshTokenResult.Errors);
        }

        RefreshTokenEnvelope refreshToken = refreshTokenResult.Value;

        if (!refreshToken.IsActive)
        {
            Result revokeWithoutNewTokenResult
                = await _unit.RefreshTokens.RevokeAsync(
                    refreshToken.RawToken, null, cancellationToken);

            if (revokeWithoutNewTokenResult.IsFailure)
            {
                return Result.Failure<LoginUserCommandResult>(refreshTokenResult.Errors);
            }

            return Result.Failure<LoginUserCommandResult>(RefreshTokenErrors.Expired);
        }

        Result<IdentityUserInfo> identityResult =
            await _identity.FindByIdAsync(refreshToken.UserId, cancellationToken);
        if (identityResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(identityResult.Errors);
        }

        Result<IReadOnlyCollection<string>> rolesResult = await _identity.GetUserRolesAsync(
            identityResult.Value.Id, cancellationToken);
        if (rolesResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(rolesResult.Errors);
        }

        Result<IReadOnlyCollection<Claim>> claimsResult = await _identity.GetUserClaimsAsync(
            identityResult.Value.Id);
        if (claimsResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(claimsResult.Errors);
        }

        IdentityUserInfo user = identityResult.Value;
        AccessToken accessToken = _tokenService.GenerateAccessToken(
            user, rolesResult.Value, claimsResult.Value);

        RefreshTokenEnvelope newRefreshToken = _tokenService.GenerateRefreshToken(user.Id);
        string newTokenHash = _tokenHasher.Hash(newRefreshToken.RawToken);

        Result revokeWithNewTokenResult = await _unit.RefreshTokens.RevokeAsync(
            oldRefreshTokenHash,
            newTokenHash,
            cancellationToken);

        if (revokeWithNewTokenResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(revokeWithNewTokenResult.Errors);
        }

        Result storeResult = await _unit.RefreshTokens.StoreAsync(
            user.Id,
            newTokenHash,
            newRefreshToken.ExpiresAt,
            cancellationToken);

        if (storeResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(storeResult.Errors);
        }

        return Result.Success(new LoginUserCommandResult(
            accessToken.RawToken,
            accessToken.ExpiresAt,
            newRefreshToken.RawToken));
    }
}
