using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.MoveTrack;

internal sealed class MoveTrackInPlaylistCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<MoveTrackInPlaylistCommand, MoveTrackInPlaylistCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<MoveTrackInPlaylistCommandResult>> Handle(
        MoveTrackInPlaylistCommand request,
        CancellationToken cancellationToken)
    {
        Playlist? playlist = await _unit.Playlists.GetByIdAsync(
            PlaylistId.From(request.PlaylistId), cancellationToken);
        if (playlist is null)
        {
            return Result.Failure<MoveTrackInPlaylistCommandResult>(PlaylistErrors.NotFound);
        }

        bool trackAvailable = await _unit.TrackReferences.IsPublishedAsync(
            request.TrackId, cancellationToken);
        if (!trackAvailable)
        {
            return Result.Failure<MoveTrackInPlaylistCommandResult>(PlaylistErrors.InvalidTrack);
        }

        if ((!_currentUser.IsAuthenticated || playlist.Collaborators.Any(c => c.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<MoveTrackInPlaylistCommandResult>(PlaylistErrors.NotOwned);
        }

        playlist.MoveTrack(
            TrackId.From(request.TrackId),
            request.TargetPositionIndex);

        return new MoveTrackInPlaylistCommandResult();
    }
}
