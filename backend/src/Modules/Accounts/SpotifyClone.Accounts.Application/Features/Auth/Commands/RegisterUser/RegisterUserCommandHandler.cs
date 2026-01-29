using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityService identity,
    IAccountsUnitOfWork unit)
    : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
{
    private readonly IIdentityService _identity = identity;
    private readonly IAccountsUnitOfWork _unit = unit;

    public async Task<Result<RegisterUserCommandResult>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<Guid> createUserResult = await CreateIdentityUserAsync(request, cancellationToken);
        if (createUserResult.IsFailure)
        {
            return Result.Failure<RegisterUserCommandResult>(createUserResult.Errors);
        }

        bool userExists = await _identity.UserExistsAsync(createUserResult.Value, cancellationToken);
        if (!userExists)
        {
            return Result.Failure<RegisterUserCommandResult>(AuthErrors.RegistrationFailed);
        }

        var userId = UserId.From(createUserResult.Value);

        Result<string> emailVerificationTokenResult
            = await _identity.GenerateEmailConfirmationTokenAsync(userId.Value);
        if (emailVerificationTokenResult.IsFailure)
        {
            return Result.Failure<RegisterUserCommandResult>(emailVerificationTokenResult.Errors);
        }

        Result<UserProfile> createUserProfileResult = await CreateUserProfileAsync(
            request, userId, cancellationToken);
        if (createUserProfileResult.IsFailure)
        {
            Result deleteResult = await _identity.DeleteUserAsync(userId.Value);
            if (deleteResult.IsFailure)
            {
                return Result.Failure<RegisterUserCommandResult>(
                    [..createUserProfileResult.Errors, ..deleteResult.Errors]);
            }

            return Result.Failure<RegisterUserCommandResult>(createUserProfileResult.Errors);
        }

        UserProfile userProfile = createUserProfileResult.Value;

        userProfile.ProcessRegistration(request.Email, emailVerificationTokenResult.Value);

        await _unit.UserProfiles.AddAsync(userProfile, cancellationToken);

        return new RegisterUserCommandResult(
            userProfile.Id.Value,
            request.Email,
            userProfile.DisplayName,
            userProfile.BirthDate,
            userProfile.Gender.Value);
    }

    private async Task<Result<Guid>> CreateIdentityUserAsync(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        bool emailExists = await _identity.EmailExistsAsync(request.Email, cancellationToken);
        if (emailExists)
        {
            return Result.Failure<Guid>(AuthErrors.EmailAlreadyInUse);
        }

        Result<Guid> createUserResult = await _identity.CreateUserAsync(
            request.Email, request.Password, cancellationToken);

        return createUserResult;
    }

    private async Task<Result<UserProfile>> CreateUserProfileAsync(
        RegisterUserCommand request,
        UserId userId,
        CancellationToken cancellationToken)
    {
        UserProfile? existingUserProfile = await _unit.UserProfiles.GetByUserIdAsync(userId, cancellationToken);
        if (existingUserProfile is not null)
        {
            return Result.Failure<UserProfile>(UserProfileErrors.AlreadyExists);
        }

        UserProfile userProfile;

        try
        {
            userProfile = UserProfile.Create(
            userId,
            request.DisplayName,
            request.BirthDate,
            Gender.From(request.Gender));
        }
        catch (DomainExceptionBase ex)
        {
            Error error = new AccountsDomainExceptionMapper().MapToError(ex);
            return Result.Failure<UserProfile>(error);
        }

        return userProfile;
    }
}
