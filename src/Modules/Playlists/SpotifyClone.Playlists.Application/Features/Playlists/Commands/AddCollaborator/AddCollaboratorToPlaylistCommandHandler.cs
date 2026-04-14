using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.AddCollaborator;

internal sealed class AddCollaboratorToPlaylistCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<AddCollaboratorToPlaylistCommand, AddCollaboratorToPlaylistCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<AddCollaboratorToPlaylistCommandResult>> Handle(
        AddCollaboratorToPlaylistCommand request,
        CancellationToken cancellationToken)
    {
        Playlist? playlist = await _unit.Playlists.GetByIdAsync(
            PlaylistId.From(request.PlaylistId), cancellationToken);
        if (playlist is null)
        {
            return Result.Failure<AddCollaboratorToPlaylistCommandResult>(PlaylistErrors.NotFound);
        }

        if (!await _unit.UserReferences.ExistsAsync(request.CollaboratorId, cancellationToken))
        {
            return Result.Failure<AddCollaboratorToPlaylistCommandResult>(PlaylistErrors.InvalidCollaborator);
        }

        if ((!_currentUser.IsAuthenticated || playlist.OwnerId.Value != _currentUser.Id) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<AddCollaboratorToPlaylistCommandResult>(PlaylistErrors.NotOwned);
        }

        playlist.AddCollaborator(UserId.From(request.CollaboratorId));

        return new AddCollaboratorToPlaylistCommandResult();
    }
}
