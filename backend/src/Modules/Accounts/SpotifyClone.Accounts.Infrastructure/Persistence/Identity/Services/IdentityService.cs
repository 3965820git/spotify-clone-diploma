using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Identity.Services;

internal sealed class IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<Result<IdentityUserInfo>> ValidateUserAsync(
        string identifier,
        string password,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(
            u => u.Email == identifier || u.UserName == identifier,
            cancellationToken);
        if (user is null)
        {
            return Result.Failure<IdentityUserInfo>(
                AuthErrors.InvalidIdentifier);
        }

        if (!await _signInManager.CanSignInAsync(user))
        {
            return Result.Failure<IdentityUserInfo>(
                AuthErrors.SignInNotAllowed);
        }

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            return Result.Failure<IdentityUserInfo>(
                AuthErrors.InvalidPassword);
        }

        return new IdentityUserInfo(
            UserId.From(user.Id),
            user.Email,
            user.PhoneNumber,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed,
            user.TwoFactorEnabled);
    }

    public async Task<Result<IdentityUserInfo>> FindByIdAsync(
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

        return new IdentityUserInfo(
            UserId.From(user.Id),
            user.Email,
            user.PhoneNumber,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed,
            user.TwoFactorEnabled);
    }

    public async Task<IdentityUserInfo?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return null;
        }

        return new IdentityUserInfo(
            UserId.From(user.Id),
            user.Email,
            user.PhoneNumber,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed,
            user.TwoFactorEnabled);
    }

    public async Task<IdentityUserInfo?> FindByPhoneNumber(
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(
            u => u.PhoneNumber == phoneNumber,
            cancellationToken);
        if (user is null)
        {
            return null;
        }

        return new IdentityUserInfo(
            UserId.From(user.Id),
            user.Email,
            user.PhoneNumber,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed,
            user.TwoFactorEnabled);
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
        string? email,
        string? password,
        string? phoneNumber,
        bool phoneNumberConfirmed = false,
        params string[] roles)
    {
        var username = Guid.NewGuid();

        var user = new ApplicationUser
        {
            Id = username,
            UserName = username.ToString(),
            Email = email,
            PhoneNumber = phoneNumber,
            PhoneNumberConfirmed = phoneNumberConfirmed,
        };

        IdentityResult createResult =
            password is null
            ? await _userManager.CreateAsync(user)
            : await _userManager.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            return Result.Failure<Guid>(
                IdentityErrorsToApplicationErrors(createResult.Errors));
        }

        foreach (string role in roles)
        {
            IdentityResult assignRoleResult = await _userManager.AddToRoleAsync(user, role);
            if (!assignRoleResult.Succeeded)
            {
                return Result.Failure<Guid>(
                    IdentityErrorsToApplicationErrors(createResult.Errors));
            }
        }

        Result claimResult = await UpdateUserSubscriptionLevelAsync(user.Id, "free");
        if (claimResult.IsFailure)
        {
            return Result.Failure<Guid>(claimResult.Errors);
        }

        return user.Id;
    }

    public async Task<Result> ChangeEmailWithPasswordAsync(
        Guid id,
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return Result.Failure(IdentityUserErrors.NotFound);
        }

        Result validateUserResult = await ValidateUserAsync(user.Email!, password, cancellationToken);
        if (validateUserResult.IsFailure)
        {
            return validateUserResult;
        }

        if (await EmailExistsAsync(email, cancellationToken))
        {
            return Result.Failure(AuthErrors.EmailAlreadyInUse);
        }

        user.Email = email;

        IdentityResult updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result.Failure(IdentityErrorsToApplicationErrors(updateResult.Errors));
        }

        return Result.Success();
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

    public async Task<Result<string>> GeneratePasswordResetTokenAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result.Failure<string>(IdentityUserErrors.NotFound);
        }

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<Result<bool>> VerifyPasswordResetTokenAsync(
        string email,
        string token,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result.Failure<bool>(IdentityUserErrors.NotFound);
        }

        return await _userManager.VerifyUserTokenAsync(
            user,
            _userManager.Options.Tokens.PasswordResetTokenProvider,
            UserManager<ApplicationUser>.ResetPasswordTokenPurpose,
            token);
    }

    public async Task<Result> ConfirmPasswordResetTokenAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result.Failure<bool>(IdentityUserErrors.NotFound);
        }

        IdentityResult resetPasswordResult = await _userManager.ResetPasswordAsync(
            user, token, newPassword);
        if (!resetPasswordResult.Succeeded)
        {
            return Result.Failure(IdentityErrorsToApplicationErrors(resetPasswordResult.Errors));
        }

        return Result.Success();
    }

    public async Task<ExternalLoginInfoEnvelope?> GetExternalLoginInfoAsync()
    {
        ExternalLoginInfo? loginInfo = await _signInManager.GetExternalLoginInfoAsync();
        if (loginInfo is null)
        {
            return null;
        }

        return new ExternalLoginInfoEnvelope(
            loginInfo.Principal.FindFirstValue(ClaimTypes.Email)!,
            loginInfo.Principal.FindFirstValue(ClaimTypes.Name)!,
            loginInfo.LoginProvider,
            loginInfo.ProviderKey,
            loginInfo.ProviderDisplayName!);
    }

    public async Task<IdentityUserInfo?> FindByLoginProviderAsync(
        string provider,
        string providerKey)
    {
        ApplicationUser? user = await _userManager.FindByLoginAsync(provider, providerKey);
        if (user is null)
        {
            return null;
        }

        return new IdentityUserInfo(
            UserId.From(user.Id),
            user.Email,
            user.PhoneNumber,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed,
            user.TwoFactorEnabled);
    }

    public async Task<Result> AddLoginAsync(
        Guid id,
        ExternalLoginInfoEnvelope loginInfo,
        CancellationToken cancellationToken = default)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return Result.Failure(IdentityUserErrors.NotFound);
        }

        IdentityResult addLoginResult = await _userManager.AddLoginAsync(
            user,
            new UserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey, loginInfo.ProviderDisplayName));
        if (!addLoginResult.Succeeded)
        {
            return Result.Failure(IdentityErrorsToApplicationErrors(addLoginResult.Errors));
        }

        return Result.Success();
    }

    public async Task<Result> UpdateUserSubscriptionLevelAsync(
        Guid userId,
        string subscriptionLevel,
        CancellationToken cancellationToken = default)
    {
        string subscriptionLevelClaimType = "subscription_level";

        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result.Failure(IdentityUserErrors.NotFound);
        }

        IList<Claim> claims = await _userManager.GetClaimsAsync(user);

        if (claims.Any(c => c.Type == subscriptionLevelClaimType))
        {
            IdentityResult removeResult = await _userManager.RemoveClaimsAsync(
                user, claims.Where(c => c.Type == subscriptionLevelClaimType));
            if (!removeResult.Succeeded)
            {
                return Result.Failure(IdentityErrorsToApplicationErrors(removeResult.Errors));
            }
        }

        IdentityResult addResult = await _userManager.AddClaimAsync(
            user, new Claim(subscriptionLevelClaimType, subscriptionLevel));
        if (!addResult.Succeeded)
        {
            return Result.Failure(IdentityErrorsToApplicationErrors(addResult.Errors));
        }

        return Result.Success();
    }

    private static Error[] IdentityErrorsToApplicationErrors(IEnumerable<IdentityError> identityErrors)
        => identityErrors.Select(e => AuthErrors.Identity(e.Code, e.Description)).ToArray();
}
