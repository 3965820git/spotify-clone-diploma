using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Otp.Verify;

internal sealed class VerifyOtpLoginCommandHandler(
    IIdentityService identity,
    IOtpCacheService otpCache,
    ITokenHasher tokenHasher,
    ITokenService tokenService,
    IAccountsUnitOfWork unit)
    : ICommandHandler<VerifyOtpLoginCommand, LoginUserCommandResult>
{
    private readonly IIdentityService _identity = identity;
    private readonly IOtpCacheService _otpCache = otpCache;
    private readonly ITokenHasher _tokenHasher = tokenHasher;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IAccountsUnitOfWork _unit = unit;

    public async Task<Result<LoginUserCommandResult>> Handle(
        VerifyOtpLoginCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Дістаємо збережений код з кешу
        string? savedCode = await _otpCache.GetOtpAsync(request.PhoneNumber, cancellationToken);

        // 2. Валідація: чи є код і чи співпадає він
        if (string.IsNullOrEmpty(savedCode) || savedCode != request.Code)
        {
            return Result.Failure<LoginUserCommandResult>(AuthErrors.InvalidPhoneNumberConfirmationToken);
        }

        // 3. Код правильний! Одразу видаляємо його, щоб уникнути повторного використання (Replay Attack)
        await _otpCache.RemoveOtpAsync(request.PhoneNumber, cancellationToken);

        // 4. Шукаємо користувача в базі
        IdentityUserInfo? userInfo = await _identity.FindByPhoneNumber(request.PhoneNumber, cancellationToken);

        // 5. ЯКЩО ЮЗЕРА НЕМАЄ -> СТВОРЮЄМО
        if (userInfo == null)
        {
            Result<Guid> createResult = await RegisterNewUserAsync(request.PhoneNumber, cancellationToken);
            if (createResult.IsFailure)
            {
                return Result.Failure<LoginUserCommandResult>(AuthErrors.RegistrationFailed);
            }

            IdentityUserInfo? newUserInfo = await _identity.FindByPhoneNumber(
                request.PhoneNumber, cancellationToken);
            if (newUserInfo is null)
            {
                return Result.Failure<LoginUserCommandResult>(IdentityUserErrors.NotFound);
            }

            userInfo = newUserInfo;
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
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        Result<Guid> createResult = await _identity.CreateUserAsync(
            null, null, phoneNumber, true, UserRoles.CalculateBy(UserRoles.Listener));
        if (createResult.IsFailure)
        {
            return createResult;
        }

        try
        {
            var userProfile = UserProfile.Create(
                UserId.From(createResult.Value),
                $"Listener_{phoneNumber[^4..]}",
                null,
                Gender.NotSpecified);

            await _unit.UserProfiles.AddAsync(userProfile, cancellationToken);
        }
        catch (DomainExceptionBase)
        {
            Result deleteResult = await _identity.DeleteUserAsync(createResult.Value);
            if (deleteResult.IsFailure)
            {
                return Result.Failure<Guid>(deleteResult.Errors);
            }

            throw;
        }

        return createResult;
    }
}
