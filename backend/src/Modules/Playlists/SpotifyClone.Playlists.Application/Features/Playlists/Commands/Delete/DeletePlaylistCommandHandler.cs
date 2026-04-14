using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.Delete;

internal sealed class DeletePlaylistCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<DeletePlaylistCommand, DeletePlaylistCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<DeletePlaylistCommandResult>> Handle(
        DeletePlaylistCommand request,
        CancellationToken cancellationToken)
    {
        Playlist? playlist = await _unit.Playlists.GetByIdAsync(
            PlaylistId.From(request.PlaylistId),
            cancellationToken);
        if (playlist is null)
        {
            return Result.Failure<DeletePlaylistCommandResult>(PlaylistErrors.NotFound);
        }

        bool isAdmin = _currentUser.IsInRole(UserRoles.Admin);
        if ((!_currentUser.IsAuthenticated || playlist.OwnerId.Value != _currentUser.Id) &&
            !isAdmin)
        {
            return Result.Failure<DeletePlaylistCommandResult>(PlaylistErrors.NotOwned);
        }

        playlist.PrepareForDeletion(isAdmin);
        await _unit.Playlists.DeleteAsync(playlist, cancellationToken);

        return new DeletePlaylistCommandResult();
    }
}
