using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Google;

internal sealed class LoginUserWithGoogleCommandHandler(
    IIdentityService identity,
    ITokenHasher tokenHasher,
    ITokenService tokenService,
    IAccountsUnitOfWork unit)
    : ICommandHandler<LoginUserWithGoogleCommand, LoginUserCommandResult>
{
    private readonly IIdentityService _identity = identity;
    private readonly ITokenHasher _tokenHasher = tokenHasher;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IAccountsUnitOfWork _unit = unit;

    public async Task<Result<LoginUserCommandResult>> Handle(
    LoginUserWithGoogleCommand request,
    CancellationToken cancellationToken)
    {
        // 1. Отримуємо дані з контексту Google
        ExternalLoginInfoEnvelope? loginInfo = await _identity.GetExternalLoginInfoAsync();
        if (loginInfo is null)
        {
            return Result.Failure<LoginUserCommandResult>(AuthErrors.SignInNotAllowed);
        }

        // 2. Спробуємо знайти юзера, який ВЖЕ пов'язаний з цим Google-аккаунтом
        IdentityUserInfo? userInfo = await _identity.FindByLoginProviderAsync(
            loginInfo.LoginProvider, loginInfo.ProviderKey);

        // 3. Якщо не знайшли — шукаємо по Email або реєструємо
        if (userInfo is null)
        {
            // Перевіряємо, чи існує юзер з таким емейлом
            IdentityUserInfo? userByEmail = await _identity.FindByEmailAsync(
                loginInfo.Email, cancellationToken);
            if (userByEmail is not null)
            {
                userInfo = userByEmail;
            }
            else
            {
                // Реєструємо нового юзера
                Result<Guid> registerResult = await RegisterNewUserAsync(loginInfo, cancellationToken);
                if (registerResult.IsFailure)
                {
                    return Result.Failure<LoginUserCommandResult>(registerResult.Errors);
                }

                // Після реєстрації нам треба отримати IdentityUserInfo для токена
                IdentityUserInfo? newUserInfo = await _identity.FindByEmailAsync(loginInfo.Email, cancellationToken);
                if (newUserInfo is null)
                {
                    return Result.Failure<LoginUserCommandResult>(IdentityUserErrors.NotFound);
                }

                userInfo = newUserInfo;
            }

            // Прив'язуємо Google до Identity-юзера
            Result linkResult = await _identity.AddLoginAsync(userInfo.Id.Value, loginInfo, cancellationToken);
            if (linkResult.IsFailure)
            {
                return Result.Failure<LoginUserCommandResult>(linkResult.Errors);
            }
        }

        // 4. Генеруємо токени для API
        Result<IReadOnlyCollection<string>> rolesResult = await _identity.GetUserRolesAsync(
            userInfo.Id, cancellationToken);
        if (rolesResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(rolesResult.Errors);
        }

        AccessToken accessToken = _tokenService.GenerateAccessToken(userInfo, rolesResult.Value);
        RefreshTokenEnvelope refreshToken = _tokenService.GenerateRefreshToken(userInfo.Id);
        string refreshTokenHash = _tokenHasher.Hash(refreshToken.RawToken);

        Result revokeResult = await _unit.RefreshTokens.RevokeAllAsync(
            userInfo.Id, refreshTokenHash, cancellationToken);
        if (revokeResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(revokeResult.Errors);
        }

        Result storeResult = await _unit.RefreshTokens.StoreAsync(
            userInfo.Id, refreshTokenHash, refreshToken.ExpiresAt, cancellationToken);
        if (storeResult.IsFailure)
        {
            return Result.Failure<LoginUserCommandResult>(storeResult.Errors);
        }

        return Result.Success(new LoginUserCommandResult(
            accessToken.RawToken,
            accessToken.ExpiresAt,
            refreshToken.RawToken));
    }

    private async Task<Result<Guid>> RegisterNewUserAsync(
        ExternalLoginInfoEnvelope loginInfo,
        CancellationToken cancellationToken = default)
    {
        Result<Guid> createUserResult = await _identity.CreateUserAsync(loginInfo.Email, null, null);
        if (createUserResult.IsFailure)
        {
            return createUserResult;
        }

        try
        {
            var userProfile = UserProfile.Create(
                UserId.From(createUserResult.Value),
                loginInfo.Name,
                null,
                Gender.NotSpecified);

            await _unit.UserProfiles.AddAsync(userProfile, cancellationToken);
        }
        catch (DomainExceptionBase)
        {
            Result deleteResult = await _identity.DeleteUserAsync(createUserResult.Value);
            if (deleteResult.IsFailure)
            {
                return Result.Failure<Guid>(deleteResult.Errors);
            }

            throw;
        }

        return createUserResult;
    }
}
