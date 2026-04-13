using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateMainArtists;

internal sealed class UpdateTrackMainArtistsCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<UpdateTrackMainArtistsCommand, UpdateTrackMainArtistsCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<UpdateTrackMainArtistsCommandResult>> Handle(
        UpdateTrackMainArtistsCommand request,
        CancellationToken cancellationToken)
    {
        Track? track = await _unit.Tracks.GetByIdAsync(
            TrackId.From(request.TrackId),
            cancellationToken);
        if (track is null)
        {
            return Result.Failure<UpdateTrackMainArtistsCommandResult>(TrackErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            track.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<UpdateTrackMainArtistsCommandResult>(AlbumErrors.NotOwned);
        }

        IEnumerable<Artist> newMainArtists = await _unit.Artists.GetAllByIdsAsync(
            request.MainArtists.Select(a => ArtistId.From(a)),
            cancellationToken);
        if (newMainArtists.Count() != request.MainArtists.Count())
        {
            return Result.Failure<UpdateTrackMainArtistsCommandResult>(ArtistErrors.NotFound);
        }

        track.UpdateMainArtists([.. newMainArtists.Select(a => a.Id)]);

        return new UpdateTrackMainArtistsCommandResult();
    }
}
