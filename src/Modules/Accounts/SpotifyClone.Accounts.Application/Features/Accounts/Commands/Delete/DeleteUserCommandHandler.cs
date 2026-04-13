using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Models;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.Delete;

internal sealed class DeleteUserCommandHandler(
    IAccountsUnitOfWork unit,
    IIdentityService identity,
    ICurrentUser currentUser)
    : ICommandHandler<DeleteUserCommand, DeleteUserCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly IIdentityService _identity = identity;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<DeleteUserCommandResult>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        if ((!_currentUser.IsAuthenticated || request.UserId != _currentUser.Id) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<DeleteUserCommandResult>(UserProfileErrors.NotOwned);
        }

        var userId = UserId.From(request.UserId);

        UserProfile? userProfile = await _unit.UserProfiles.GetByIdAsync(userId, cancellationToken);
        if (userProfile is null)
        {
            return Result.Failure<DeleteUserCommandResult>(UserProfileErrors.NotFound);
        }

        Result<IdentityUserInfo> identityUserResult = await _identity.FindByIdAsync(userId, cancellationToken);
        if (identityUserResult.IsFailure)
        {
            return Result.Failure<DeleteUserCommandResult>(identityUserResult.Errors);
        }

        userProfile.PrepareForDeletion();
        await _unit.UserProfiles.DeleteAsync(userProfile, cancellationToken);

        Result deleteIdentityResult = await _identity.DeleteUserAsync(userId.Value);
        if (deleteIdentityResult.IsFailure)
        {
            return Result.Failure<DeleteUserCommandResult>(deleteIdentityResult.Errors);
        }

        return new DeleteUserCommandResult();
    }
}
