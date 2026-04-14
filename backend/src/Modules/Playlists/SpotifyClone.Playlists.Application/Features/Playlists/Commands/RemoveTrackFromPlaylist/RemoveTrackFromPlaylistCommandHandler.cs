using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.RemoveTrackFromPlaylist;

internal sealed class RemoveTrackFromPlaylistCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<RemoveTrackFromPlaylistCommand, RemoveTrackFromPlaylistCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<RemoveTrackFromPlaylistCommandResult>> Handle(
        RemoveTrackFromPlaylistCommand request,
        CancellationToken cancellationToken)
    {
        Playlist? playlist = await _unit.Playlists.GetByIdAsync(
            PlaylistId.From(request.PlaylistId), cancellationToken);
        if (playlist is null)
        {
            return Result.Failure<RemoveTrackFromPlaylistCommandResult>(PlaylistErrors.NotFound);
        }

        bool isAdmin = _currentUser.IsInRole(UserRoles.Admin);
        if ((!_currentUser.IsAuthenticated || playlist.Collaborators.Any(c => c.Value != _currentUser.Id)) &&
            !isAdmin)
        {
            return Result.Failure<RemoveTrackFromPlaylistCommandResult>(PlaylistErrors.NotOwned);
        }

        playlist.RemoveTrack(
            TrackId.From(request.TrackId),
            UserId.From(_currentUser.Id),
            isAdmin);

        return new RemoveTrackFromPlaylistCommandResult();
    }
}
