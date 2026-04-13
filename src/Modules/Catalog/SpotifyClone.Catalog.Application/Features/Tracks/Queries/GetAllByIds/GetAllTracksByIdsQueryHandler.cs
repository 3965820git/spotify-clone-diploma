using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetAllByIds;

internal sealed class GetAllTracksByIdsQueryHandler(
    ITrackReadService trackReadService,
    IArtistReadService artistReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetAllTracksByIdsQuery, TrackList>
{
    private readonly ITrackReadService _trackReadService = trackReadService;
    private readonly IArtistReadService _artistReadService = artistReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<TrackList>> Handle(
        GetAllTracksByIdsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<TrackSummary> tracks = await _trackReadService.GetAllByIdsAsync(
            request.TrackIds.Select(id => TrackId.From(id)), cancellationToken);
        if (tracks is null)
        {
            return Result.Failure<TrackList>(TrackErrors.NotFound);
        }

        var artistIds = tracks
            .SelectMany(t => t.MainArtists.Concat(t.FeaturedArtists))
            .Distinct()
            .Select(id => ArtistId.From(id))
            .ToList();

        IEnumerable<ArtistSummary> artists = await _artistReadService.GetAllAsync(
            artistIds, cancellationToken);

        if (!_currentUser.IsAuthenticated &&
            !artists.Any(a => a.OwnerId == _currentUser.Id) &&
            _currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<TrackList>(TrackErrors.NotOwned);
        }

        return new TrackList(tracks);
    }
}
