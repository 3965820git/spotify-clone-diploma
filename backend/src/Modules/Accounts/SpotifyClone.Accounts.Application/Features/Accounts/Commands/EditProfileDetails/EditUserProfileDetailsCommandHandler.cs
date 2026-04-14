using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.EditProfileDetails;

internal sealed class EditUserProfileDetailsCommandHandler(
    IAccountsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<EditUserProfileDetailsCommand, EditUserProfileDetailsCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<EditUserProfileDetailsCommandResult>> Handle(
        EditUserProfileDetailsCommand request,
        CancellationToken cancellationToken)
    {
        UserProfile? userProfile = await _unit.UserProfiles.GetByIdAsync(
            UserId.From(request.UserId),
            cancellationToken);
        if (userProfile is null)
        {
            return Result.Failure<EditUserProfileDetailsCommandResult>(UserProfileErrors.NotFound);
        }

        if ((!_currentUser.IsAuthenticated || userProfile.Id.Value != _currentUser.Id) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<EditUserProfileDetailsCommandResult>(UserProfileErrors.NotOwned);
        }

        userProfile.EditDetails(request.DisplayName);

        return new EditUserProfileDetailsCommandResult();
    }
}
