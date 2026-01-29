using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithPassword;

internal sealed class LoginWithPasswordCommandHandler(
    IAccountsUnitOfWork unit,
    IIdentityService identity,
    ITokenService tokenService,
    ITokenHasher tokenHasher)
    : ICommandHandler<LoginWithPasswordCommand, LoginWithPasswordCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly IIdentityService _identity = identity;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ITokenHasher _tokenHasher = tokenHasher;

    public async Task<Result<LoginWithPasswordCommandResult>> Handle(LoginWithPasswordCommand request, CancellationToken cancellationToken)
    {
        Result<IdentityUserInfo> identityResult = await _identity.ValidateUserAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (identityResult.IsFailure)
        {
            return Result.Failure<LoginWithPasswordCommandResult>(identityResult.Errors);
        }

        UserId userId = identityResult.Value.UserId;

        AccessToken accessToken = _tokenService.GenerateAccessToken(identityResult.Value, ["User"]);
        RefreshTokenEnvelope refreshToken = _tokenService.GenerateRefreshToken(userId);
        string refreshTokenHash = _tokenHasher.Hash(refreshToken.RawToken);

        Result revokeResult = await _unit.RefreshTokens.RevokeAllAsync(userId, refreshTokenHash, cancellationToken);
        if (revokeResult.IsFailure)
        {
            return Result.Failure<LoginWithPasswordCommandResult>(revokeResult.Errors);
        }

        Result storeResult = await _unit.RefreshTokens.StoreAsync(
            userId, refreshTokenHash, refreshToken.ExpiresAt, cancellationToken);
        if (storeResult.IsFailure)
        {
            return Result.Failure<LoginWithPasswordCommandResult>(storeResult.Errors);
        }

        return Result.Success(new LoginWithPasswordCommandResult(
            accessToken.RawToken,
            refreshToken.RawToken));
    }
}
