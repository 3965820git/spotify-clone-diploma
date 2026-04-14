using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Genres;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateGenres;

internal sealed class UpdateTrackGenresCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<UpdateTrackGenresCommand, UpdateTrackGenresCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<UpdateTrackGenresCommandResult>> Handle(
        UpdateTrackGenresCommand request,
        CancellationToken cancellationToken)
    {
        Track? track = await _unit.Tracks.GetByIdAsync(
            TrackId.From(request.TrackId),
            cancellationToken);
        if (track is null)
        {
            return Result.Failure<UpdateTrackGenresCommandResult>(TrackErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            track.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<UpdateTrackGenresCommandResult>(AlbumErrors.NotOwned);
        }

        IEnumerable<Genre> newGenres = await _unit.Genres.GetByIdsAsync(
            request.Genres.Select(a => GenreId.From(a)),
            cancellationToken);
        if (newGenres.Count() != request.Genres.Count())
        {
            return Result.Failure<UpdateTrackGenresCommandResult>(GenreErrors.NotFound);
        }

        track.UpdateGenres([.. newGenres.Select(a => a.Id)]);

        return new UpdateTrackGenresCommandResult();
    }
}
