using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Albums.Queries.GetAllByArtist;

internal sealed class GetAllAlbumsByArtistQueryHandler(
    IArtistReadService artistReadService,
    IAlbumReadService albumReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetAllAlbumsByArtistQuery, AlbumList>
{
    private readonly IArtistReadService _artistReadService = artistReadService;
    private readonly IAlbumReadService _albumReadService = albumReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<AlbumList>> Handle(
        GetAllAlbumsByArtistQuery request,
        CancellationToken cancellationToken)
    {
        var artistId = ArtistId.From(request.ArtistId);

        ArtistDetails? artist = await _artistReadService.GetDetailsAsync(
            artistId, cancellationToken);
        if (artist is null)
        {
            return Result.Failure<AlbumList>(ArtistErrors.NotFound);
        }

        IEnumerable<AlbumSummary> albums =
            _currentUser.IsAuthenticated && artist.OwnerId == _currentUser.Id ||
            _currentUser.IsInRole(UserRoles.Admin)
            ? await _albumReadService.GetAllByArtistIdAsync(artistId, cancellationToken)
            : await _albumReadService.GetAllPublishedByArtistIdAsync(artistId, cancellationToken);

        return new AlbumList(albums.ToList().AsReadOnly());
    }
}
