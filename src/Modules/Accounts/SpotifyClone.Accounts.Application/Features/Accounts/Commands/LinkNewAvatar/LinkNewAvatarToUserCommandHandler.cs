using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Domain.Aggregates.Users.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.LinkNewAvatar;

internal sealed class LinkNewAvatarToUserCommandHandler(
    IAccountsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<LinkNewAvatarToUserCommand, LinkNewAvatarToUserCommandResult>
{
    private readonly IAccountsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<LinkNewAvatarToUserCommandResult>> Handle(
        LinkNewAvatarToUserCommand request,
        CancellationToken cancellationToken)
    {
        UserProfile? userProfile = await _unit.UserProfiles.GetByIdAsync(
            UserId.From(request.UserId),
            cancellationToken);
        if (userProfile is null)
        {
            return Result.Failure<LinkNewAvatarToUserCommandResult>(UserProfileErrors.NotFound);
        }

        if ((!_currentUser.IsAuthenticated || userProfile.Id.Value != _currentUser.Id) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<LinkNewAvatarToUserCommandResult>(UserProfileErrors.NotOwned);
        }

        userProfile.LinkNewAvatar(new AvatarImage(
            ImageId.From(request.ImageId),
            request.ImageWidth,
            request.ImageHeight,
            ImageFileType.From(request.ImageFileType),
            request.ImageSizeInBytes));

        return new LinkNewAvatarToUserCommandResult();
    }
}
