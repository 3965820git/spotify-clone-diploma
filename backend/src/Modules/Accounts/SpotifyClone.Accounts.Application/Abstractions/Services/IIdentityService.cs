using System.Security.Claims;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface IIdentityService
{
    Task<Result<IdentityUserInfo>> ValidateUserAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<Result<IdentityUserInfo>> GetUserInfoAsync(
        UserId userId,
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
}
