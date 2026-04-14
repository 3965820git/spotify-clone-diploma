using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Albums;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.UpdateMainArtists;

internal sealed class UpdateAlbumMainArtistsCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<UpdateAlbumMainArtistsCommand, UpdateAlbumMainArtistsCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<UpdateAlbumMainArtistsCommandResult>> Handle(
        UpdateAlbumMainArtistsCommand request,
        CancellationToken cancellationToken)
    {
        Album? album = await _unit.Albums.GetByIdAsync(
            AlbumId.From(request.AlbumId),
            cancellationToken);
        if (album is null)
        {
            return Result.Failure<UpdateAlbumMainArtistsCommandResult>(AlbumErrors.NotFound);
        }

        IEnumerable<Artist> mainArtists = await _unit.Artists.GetAllByIdsAsync(
            album.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || mainArtists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<UpdateAlbumMainArtistsCommandResult>(AlbumErrors.NotOwned);
        }

        IEnumerable<Artist> newMainArtists = await _unit.Artists.GetAllByIdsAsync(
            request.MainArtists.Select(a => ArtistId.From(a)),
            cancellationToken);
        if (newMainArtists.Count() != request.MainArtists.Count())
        {
            return Result.Failure<UpdateAlbumMainArtistsCommandResult>(ArtistErrors.NotFound);
        }

        album.UpdateMainArtists([.. newMainArtists.Select(a => a.Id)]);

        return new UpdateAlbumMainArtistsCommandResult();
    }
}
