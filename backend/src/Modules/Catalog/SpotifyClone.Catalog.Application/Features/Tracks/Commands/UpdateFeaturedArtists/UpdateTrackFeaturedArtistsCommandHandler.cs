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

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateFeaturedArtists;

internal sealed class UpdateTrackFeaturedArtistsCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<UpdateTrackFeaturedArtistsCommand, UpdateTrackFeaturedArtistsCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<UpdateTrackFeaturedArtistsCommandResult>> Handle(
        UpdateTrackFeaturedArtistsCommand request,
        CancellationToken cancellationToken)
    {
        Track? track = await _unit.Tracks.GetByIdAsync(
            TrackId.From(request.TrackId),
            cancellationToken);
        if (track is null)
        {
            return Result.Failure<UpdateTrackFeaturedArtistsCommandResult>(TrackErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            track.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<UpdateTrackFeaturedArtistsCommandResult>(AlbumErrors.NotOwned);
        }

        IEnumerable<Artist> newFeaturedArtists = await _unit.Artists.GetAllByIdsAsync(
            request.FeaturedArtists.Select(a => ArtistId.From(a)),
            cancellationToken);
        if (newFeaturedArtists.Count() != request.FeaturedArtists.Count())
        {
            return Result.Failure<UpdateTrackFeaturedArtistsCommandResult>(ArtistErrors.NotFound);
        }

        track.UpdateFeaturedArtists([.. newFeaturedArtists.Select(a => a.Id)]);

        return new UpdateTrackFeaturedArtistsCommandResult();
    }
}
