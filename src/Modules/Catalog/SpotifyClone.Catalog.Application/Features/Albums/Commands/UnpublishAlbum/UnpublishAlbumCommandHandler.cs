using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.UnpublishAlbum;

internal sealed class UnpublishAlbumCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser,
    AlbumTrackDomainService albumTrackDomainService)
    : ICommandHandler<UnpublishAlbumCommand, UnpublishAlbumCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;

    public async Task<Result<UnpublishAlbumCommandResult>> Handle(
        UnpublishAlbumCommand request,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
            AlbumId.From(request.AlbumId),
            cancellationToken);
        if (album is null)
        {
            return Result.Failure<UnpublishAlbumCommandResult>(AlbumErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            album.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<UnpublishAlbumCommandResult>(AlbumErrors.NotOwned);
        }

        album.Unpublish();
        _albumTrackDomainService.TryMarkAlbumAsReadyToPublish(album);

        return new UnpublishAlbumCommandResult();
    }
}
