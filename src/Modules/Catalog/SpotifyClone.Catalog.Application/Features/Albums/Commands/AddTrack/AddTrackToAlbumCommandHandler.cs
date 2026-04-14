using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Catalog.Domain.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.AddTrack;

internal sealed class AddTrackToAlbumCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser,
    AlbumTrackDomainService albumTrackDomainService)
    : ICommandHandler<AddTrackToAlbumCommand, AddTrackToAlbumCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;

    public async Task<Result<AddTrackToAlbumCommandResult>> Handle(
        AddTrackToAlbumCommand request,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
            AlbumId.From(request.AlbumId), cancellationToken);
        if (album is null)
        {
            return Result.Failure<AddTrackToAlbumCommandResult>(AlbumErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            album.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<AddTrackToAlbumCommandResult>(AlbumErrors.NotOwned);
        }

        var trackId = TrackId.From(request.TrackId);

        Track? track = await _unit.Tracks.GetByIdAsync(
            trackId, cancellationToken);
        if (track is null)
        {
            return Result.Failure<AddTrackToAlbumCommandResult>(TrackErrors.NotFound);
        }

        album.AddTrack(trackId);
        _albumTrackDomainService.TryMarkAlbumAsReadyToPublish(album);
        _albumTrackDomainService.ReevaluateAlbumType(album);

        return new AddTrackToAlbumCommandResult();
    }
}
