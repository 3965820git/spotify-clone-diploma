using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.EditPersonalInfo;

internal sealed class EditUserPersonalInfoCommandHandler(
    IAccountsUnitOfWork unit,
    IIdentityService identity,
    ICurrentUser currentUser,
    IAccountVerificationService verificationService)
    : ICommandHandler<EditUserPersonalInfoCommand, EditUserPersonalInfoCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly IIdentityService _identity = identity;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IAccountVerificationService _verificationService = verificationService;

    public async Task<Result<EditUserPersonalInfoCommandResult>> Handle(
        EditUserPersonalInfoCommand request,
        CancellationToken cancellationToken)
    {
        if ((!_currentUser.IsAuthenticated || request.UserId != _currentUser.Id) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<EditUserPersonalInfoCommandResult>(UserProfileErrors.NotOwned);
        }

        UserProfile? userProfile = await _unit.UserProfiles.GetByIdAsync(
            UserId.From(request.UserId),
            cancellationToken);
        if (userProfile is null)
        {
            return Result.Failure<EditUserPersonalInfoCommandResult>(UserProfileErrors.NotFound);
        }

        userProfile.EditPersonalInfo(Gender.From(request.Gender), request.BirthDateUtc);

        Result<IdentityUserInfo> userInfoResult = await _identity.FindByIdAsync(
            UserId.From(request.UserId), cancellationToken);
        if (userInfoResult.IsFailure)
        {
            return Result.Failure<EditUserPersonalInfoCommandResult>(userInfoResult.Errors);
        }
        IdentityUserInfo userInfo = userInfoResult.Value;

        if (userInfo.Email is not null && request.Email != userInfo.Email)
        {
            string oldEmail = userInfo.Email;

            Result changeEmailResult = await ChangeEmailAsync(request, cancellationToken);
            if (changeEmailResult.IsFailure)
            {
                return Result.Failure<EditUserPersonalInfoCommandResult>(changeEmailResult.Errors);
            }

            Result emailSendResult = await _verificationService.SendEmailChangedEmailAsync(
                oldEmail, request.Email, userProfile.DisplayName, cancellationToken);
            if (emailSendResult.IsFailure)
            {
                return Result.Failure<EditUserPersonalInfoCommandResult>(emailSendResult.Errors);
            }
        }

        return new EditUserPersonalInfoCommandResult();
    }

    private async Task<Result> ChangeEmailAsync(
        EditUserPersonalInfoCommand request,
        CancellationToken cancellationToken = default)
    {
        if (request.Password is null)
        {
            return Result.Failure(AuthErrors.InvalidPassword);
        }

        Result changeEmailResult = await _identity.ChangeEmailWithPasswordAsync(
            request.UserId, request.Email, request.Password, cancellationToken);
        if (changeEmailResult.IsFailure)
        {
            return Result.Failure(changeEmailResult.Errors);
        }

        return changeEmailResult;
    }
}
