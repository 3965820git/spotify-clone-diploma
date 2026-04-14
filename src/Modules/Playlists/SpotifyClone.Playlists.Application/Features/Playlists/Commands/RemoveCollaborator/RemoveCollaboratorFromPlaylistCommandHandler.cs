using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.RemoveCollaborator;

internal sealed class RemoveCollaboratorFromPlaylistCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<RemoveCollaboratorFromPlaylistCommand, RemoveCollaboratorFromPlaylistCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<RemoveCollaboratorFromPlaylistCommandResult>> Handle(
        RemoveCollaboratorFromPlaylistCommand request,
        CancellationToken cancellationToken)
    {
        Playlist? playlist = await _unit.Playlists.GetByIdAsync(
            PlaylistId.From(request.PlaylistId), cancellationToken);
        if (playlist is null)
        {
            return Result.Failure<RemoveCollaboratorFromPlaylistCommandResult>(PlaylistErrors.NotFound);
        }

        if ((!_currentUser.IsAuthenticated || playlist.OwnerId.Value != _currentUser.Id) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<RemoveCollaboratorFromPlaylistCommandResult>(PlaylistErrors.NotOwned);
        }

        playlist.RemoveCollaborator(UserId.From(request.CollaboratorId));

        return new RemoveCollaboratorFromPlaylistCommandResult();
    }
}
