using System.Security.Claims;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Password;

internal sealed class LoginUserWithPasswordCommandHandler(
    IAccountsUnitOfWork unit,
    IIdentityService identity,
    ITokenService tokenService,
    ITokenHasher tokenHasher)
    : ICommandHandler<LoginUserWithPasswordCommand, LoginUserCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly IIdentityService _identity = identity;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ITokenHasher _tokenHasher = tokenHasher;

    public async Task<Result<LoginUserCommandResult>> Handle(LoginUserWithPasswordCommand request, CancellationToken cancellationToken)
    {
        Result<IdentityUserInfo> identityResult = await _identity.ValidateUserAsync(
            request.Identifier,
            request.Password,
            cancellationToken);
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

        UserId userId = identityResult.Value.Id;

        AccessToken accessToken = _tokenService.GenerateAccessToken(
            identityResult.Value, rolesResult.Value, claimsResult.Value);
        RefreshTokenEnvelope refreshToken = _tokenService.GenerateRefreshToken(userId);
        string refreshTokenHash = _tokenHasher.Hash(refreshToken.RawToken);

        Result revokeResult = await _unit.RefreshTokens.RevokeAllAsync(userId, refreshTokenHash, cancellationToken);
        if (revokeResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(revokeResult.Errors);
        }

        Result storeResult = await _unit.RefreshTokens.StoreAsync(
            userId, refreshTokenHash, refreshToken.ExpiresAt, cancellationToken);
        if (storeResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(storeResult.Errors);
        }

        return Result.Success(new LoginUserCommandResult(
            accessToken.RawToken,
            accessToken.ExpiresAt,
            refreshToken.RawToken));
    }
}
