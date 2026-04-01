using System.Security.Claims;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface IIdentityService
{
    Task<Result<IdentityUserInfo>> ValidateUserAsync(
        string identifier,
        string password,
        CancellationToken cancellationToken = default);

    Task<Result<IdentityUserInfo>> FindByIdAsync(
        UserId userId,
        CancellationToken cancellationToken = default);

    Task<IdentityUserInfo?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<IdentityUserInfo?> FindByPhoneNumber(
        string phoneNumber,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyCollection<string>>> GetUserRolesAsync(
        UserId userId,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyCollection<Claim>>> GetUserClaimsAsync(
        UserId userId);

    Task<Result<bool>> IsTwoFactorEnabledAsync(
        UserId userId,
        CancellationToken cancellationToken = default);

    Task<bool> EmailExistsAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<bool> UserExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Result<Guid>> CreateUserAsync(
        string? email,
        string? password,
        string? phoneNumber,
        bool phoneNumberConfirmed = false,
        params string[] roles);

    Task<Result> ChangeEmailWithPasswordAsync(
        Guid id,
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<Result> DeleteUserAsync(Guid id);

    Task<Result<string>> GenerateEmailConfirmationTokenAsync(
        Guid userId);

    Task<Result> ConfirmEmailAsync(
        Guid userId,
        string token);

    Task<Result<string>> GeneratePhoneNumberConfirmationTokenAsync(
        Guid userId,
        string phoneNumber);

    Task<Result> ConfirmPhoneNumberAsync(
        Guid userId,
        string phoneNumber,
        string token);

    Task<Result<string>> GeneratePasswordResetTokenAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> VerifyPasswordResetTokenAsync(
        string email,
        string token,
        CancellationToken cancellationToken = default);

    Task<Result> ConfirmPasswordResetTokenAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken = default);

    Task<ExternalLoginInfoEnvelope?> GetExternalLoginInfoAsync();

    Task<IdentityUserInfo?> FindByLoginProviderAsync(
        string provider,
        string providerKey);

    Task<Result> AddLoginAsync(
        Guid id,
        ExternalLoginInfoEnvelope loginInfo,
        CancellationToken cancellationToken = default);

    Task<Result> UpdateUserSubscriptionLevelAsync(
        Guid userId,
        string subscriptionLevel,
        CancellationToken cancellationToken = default);
}
