using SpotifyClone.Playlists.Application.Abstractions;
using SpotifyClone.Playlists.Application.Errors;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.LinkNewCover;

internal sealed class LinkNewCoverToPlaylistCommandHandler(
    IPlaylistsUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<LinkNewCoverToPlaylistCommand, LinkNewCoverToPlaylistCommandResult>
{
    private readonly IPlaylistsUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<LinkNewCoverToPlaylistCommandResult>> Handle(
        LinkNewCoverToPlaylistCommand request,
        CancellationToken cancellationToken)
    {
        Playlist? playlist = await _unit.Playlists.GetByIdAsync(
            PlaylistId.From(request.PlaylistId),
            cancellationToken);
        if (playlist is null)
        {
            return Result.Failure<LinkNewCoverToPlaylistCommandResult>(PlaylistErrors.NotFound);
        }

        bool isAdmin = _currentUser.IsInRole(UserRoles.Admin);
        if ((!_currentUser.IsAuthenticated || playlist.OwnerId.Value != _currentUser.Id) &&
            !isAdmin)
        {
            return Result.Failure<LinkNewCoverToPlaylistCommandResult>(PlaylistErrors.NotOwned);
        }

        playlist.LinkNewCover(new PlaylistCoverImage(
            ImageId.From(request.ImageId),
            request.ImageWidth,
            request.ImageHeight,
            ImageFileType.From(request.ImageFileType),
            request.ImageSizeInBytes),
            isAdmin);

        return new LinkNewCoverToPlaylistCommandResult();
    }
}
