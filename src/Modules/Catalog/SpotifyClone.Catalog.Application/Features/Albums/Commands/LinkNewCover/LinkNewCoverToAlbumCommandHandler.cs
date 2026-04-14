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
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.LinkNewCover;

internal sealed class LinkNewCoverToAlbumCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser,
    AlbumTrackDomainService albumTrackDomainService)
    : ICommandHandler<LinkNewCoverToAlbumCommand, LinkNewCoverToAlbumCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly AlbumTrackDomainService _albumTrackDomainService = albumTrackDomainService;

    public async Task<Result<LinkNewCoverToAlbumCommandResult>> Handle(
        LinkNewCoverToAlbumCommand request,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
            AlbumId.From(request.AlbumId),
            cancellationToken);
        if (album is null)
        {
            return Result.Failure<LinkNewCoverToAlbumCommandResult>(AlbumErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            album.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<LinkNewCoverToAlbumCommandResult>(AlbumErrors.NotOwned);
        }

        album.LinkNewCover(new AlbumCoverImage(
            ImageId.From(request.ImageId),
            request.ImageWidth,
            request.ImageHeight,
            ImageFileType.From(request.ImageFileType),
            request.ImageSizeInBytes));

        _albumTrackDomainService.TryMarkAlbumAsReadyToPublish(album);

        return new LinkNewCoverToAlbumCommandResult();
    }
}
