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

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.PublishAlbum;

internal sealed class PublishAlbumCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser,
    AlbumTrackDomainService albumTrackDomainService)
    : ICommandHandler<PublishAlbumCommand, PublishAlbumCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;

    public async Task<Result<PublishAlbumCommandResult>> Handle(
        PublishAlbumCommand request,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
            AlbumId.From(request.AlbumId),
            cancellationToken);
        if (album is null)
        {
            return Result.Failure<PublishAlbumCommandResult>(AlbumErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            album.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<PublishAlbumCommandResult>(AlbumErrors.NotOwned);
        }

        album.Publish(request.ReleaseDate);
        _albumTrackDomainService.ReevaluateAlbumType(album);

        return new PublishAlbumCommandResult();
    }
}
