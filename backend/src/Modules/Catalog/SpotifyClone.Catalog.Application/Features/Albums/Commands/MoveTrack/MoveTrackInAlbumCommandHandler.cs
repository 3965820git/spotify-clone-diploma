using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.MoveTrack;

internal sealed class MoveTrackInAlbumCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<MoveTrackInAlbumCommand, MoveTrackInAlbumCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<MoveTrackInAlbumCommandResult>> Handle(
        MoveTrackInAlbumCommand request,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
            AlbumId.From(request.AlbumId), cancellationToken);
        if (album is null)
        {
            return Result.Failure<MoveTrackInAlbumCommandResult>(AlbumErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            album.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<MoveTrackInAlbumCommandResult>(AlbumErrors.NotOwned);
        }

        Track? track = await _unit.Tracks.GetByIdAsync(
            TrackId.From(request.TrackId), cancellationToken);
        if (track is null)
        {
            return Result.Failure<MoveTrackInAlbumCommandResult>(TrackErrors.NotFound);
        }

        album.MoveTrack(track.Id, request.TargetPositionIndex);

        return new MoveTrackInAlbumCommandResult();
    }
}
