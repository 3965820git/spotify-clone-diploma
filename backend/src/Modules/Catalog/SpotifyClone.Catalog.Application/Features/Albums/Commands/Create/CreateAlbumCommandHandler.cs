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

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.Create;

internal sealed class CreateAlbumCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<CreateAlbumCommand, CreateAlbumCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<CreateAlbumCommandResult>> Handle(
        CreateAlbumCommand request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            request.MainArtists.Select(a => ArtistId.From(a)),
            cancellationToken);

        if (artists.Count() != request.MainArtists.Count())
        {
            return Result.Failure<CreateAlbumCommandResult>(ArtistErrors.NotFound);
        }

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<CreateAlbumCommandResult>(AlbumErrors.NotOwned);
        }

        var album = Album.Create(
            AlbumId.From(Guid.NewGuid()),
            request.Title,
            artists.Select(a => a.Id));

        await _unit.Albums.AddAsync(album, cancellationToken);

        return new CreateAlbumCommandResult(album.Id.Value);
    }
}
