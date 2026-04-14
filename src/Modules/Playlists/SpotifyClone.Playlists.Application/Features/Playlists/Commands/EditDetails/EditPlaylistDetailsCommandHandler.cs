using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.EditDetails;

internal sealed class EditPlaylistDetailsCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<EditPlaylistDetailsCommand, EditPlaylistDetailsCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<EditPlaylistDetailsCommandResult>> Handle(
        EditPlaylistDetailsCommand request,
        CancellationToken cancellationToken)
    {
        Playlist? playlist = await _unit.Playlists.GetByIdAsync(
            PlaylistId.From(request.PlaylistId),
            cancellationToken);
        if (playlist is null)
        {
            return Result.Failure<EditPlaylistDetailsCommandResult>(PlaylistErrors.NotFound);
        }

        bool isAdmin = _currentUser.IsInRole(UserRoles.Admin);
        if ((!_currentUser.IsAuthenticated || playlist.OwnerId.Value != _currentUser.Id) &&
            !isAdmin)
        {
            return Result.Failure<EditPlaylistDetailsCommandResult>(PlaylistErrors.NotOwned);
        }

        playlist.EditDetails(
            request.Name,
            request.Description,
            request.IsPublic,
            isAdmin);

        return new EditPlaylistDetailsCommandResult();
    }
}
