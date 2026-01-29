using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Identity.Services;

internal sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result<IdentityUserInfo>> ValidateUserAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result.Failure<IdentityUserInfo>(
                AuthErrors.InvalidEmail);
        }

        if (!await _signInManager.CanSignInAsync(user))
        {
            return Result.Failure<IdentityUserInfo>(
                AuthErrors.SignInNotAllowed);
        }

        bool passwordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!passwordValid)
        {
            return Result.Failure<IdentityUserInfo>(
                AuthErrors.InvalidPassword);
        }

        bool requiresTwoFactor = await _userManager.GetTwoFactorEnabledAsync(user);

        return new IdentityUserInfo(
            UserId.From(user.Id),
            user.Email!,
            user.EmailConfirmed,
            requiresTwoFactor);
    }

    public async Task<Result<IdentityUserInfo>> GetUserInfoAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.Value.ToString());

        if (user is null)
        {
            return Result.Failure<IdentityUserInfo>(IdentityUserErrors.NotFound);
        }

        if (!await _signInManager.CanSignInAsync(user))
        {
            return Result.Failure<IdentityUserInfo>(AuthErrors.SignInNotAllowed);
        }

        bool requiresTwoFactor = await _userManager.GetTwoFactorEnabledAsync(user);

        return new IdentityUserInfo(
            UserId.From(user.Id),
            user.Email!,
            user.EmailConfirmed,
            requiresTwoFactor);
    }

    public async Task<Result<IReadOnlyCollection<string>>> GetUserRolesAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.Value.ToString());

        if (user is null)
        {
            return Result.Failure<IReadOnlyCollection<string>>(IdentityUserErrors.NotFound);
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        return roles.AsReadOnly();
    }

    public async Task<Result<IReadOnlyCollection<Claim>>> GetUserClaimsAsync(
        UserId userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.Value.ToString());

        if (user is null)
        {
            return Result.Failure<IReadOnlyCollection<Claim>>(IdentityUserErrors.NotFound);
        }

        IList<Claim> claims = await _userManager.GetClaimsAsync(user);

        return claims.AsReadOnly();
    }

    public async Task<Result<bool>> IsTwoFactorEnabledAsync(
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.Value.ToString());

        if (user is null)
        {
            return Result.Failure<bool>(IdentityUserErrors.NotFound);
        }

        return await _userManager.GetTwoFactorEnabledAsync(user);
    }

    public async Task<bool> EmailExistsAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UserExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return false;
        }

        return true;
    }

    public async Task<Result<Guid>> CreateUserAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email
        };

        IdentityResult result =
            await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return Result.Failure<Guid>(
                IdentityErrorsToApplicationErrors(result.Errors));
        }

        return user.Id;
    }

    public async Task<Result> DeleteUserAsync(Guid id)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return Result.Failure(IdentityUserErrors.NotFound);
        }

        IdentityResult result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return Result.Failure<Guid>(IdentityErrorsToApplicationErrors(result.Errors));
        }

        return Result.Success();
    }

    public async Task<Result<string>> GenerateEmailConfirmationTokenAsync(
        Guid userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result.Failure<string>(IdentityUserErrors.NotFound);
        }

        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<Result> ConfirmEmailAsync(
        Guid userId,
        string token)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result.Failure(IdentityUserErrors.NotFound);
        }

        if (user.EmailConfirmed)
        {
            return Result.Failure(AuthErrors.EmailAlreadyConfirmed);
        }

        IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return Result.Failure(AuthErrors.InvalidEmailConfirmationToken);
        }

        return Result.Success();
    }

    public async Task<Result<string>> GeneratePhoneNumberConfirmationTokenAsync(
        Guid userId,
        string phoneNumber)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result.Failure<string>(IdentityUserErrors.NotFound);
        }

        return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
    }

    public async Task<Result> ConfirmPhoneNumberAsync(
        Guid userId,
        string phoneNumber,
        string token)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result.Failure(IdentityUserErrors.NotFound);
        }

        if (user.PhoneNumberConfirmed)
        {
            return Result.Failure(AuthErrors.PhoneNumberAlreadyConfirmed);
        }

        IdentityResult result = await _userManager.ChangePhoneNumberAsync(user, phoneNumber, token);
        if (!result.Succeeded)
        {
            return Result.Failure(AuthErrors.InvalidPhoneNumberConfirmationToken);
        }

        return Result.Success();
    }

    private static Error[] IdentityErrorsToApplicationErrors(IEnumerable<IdentityError> identityErrors)
        => identityErrors.Select(e => AuthErrors.Identity(e.Code, e.Description)).ToArray();
}
