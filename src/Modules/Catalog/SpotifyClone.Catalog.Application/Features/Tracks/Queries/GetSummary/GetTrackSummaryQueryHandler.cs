using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetSummary;

internal sealed class GetTrackSummaryQueryHandler(
    ITrackReadService trackReadService,
    IArtistReadService artistReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetTrackSummaryQuery, TrackSummary>
{
    private readonly ITrackReadService _trackReadService = trackReadService;
    private readonly IArtistReadService _artistReadService = artistReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<TrackSummary>> Handle(
        GetTrackSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var trackId = TrackId.From(request.TrackId);

        TrackSummary? track = await _trackReadService.GetSummaryAsync(trackId, cancellationToken);
        if (track is null)
        {
            return Result.Failure<TrackSummary>(TrackErrors.NotFound);
        }

        IEnumerable<ArtistSummary> artists = await _artistReadService.GetAllAsync(
            [ ..track.MainArtists.Select(id => ArtistId.From(id)),
              ..track.FeaturedArtists.Select(id => ArtistId.From(id)) ],
            cancellationToken);

        if (!_currentUser.IsAuthenticated &&
            !artists.Any(a => a.OwnerId == _currentUser.Id) &&
            _currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<TrackSummary>(TrackErrors.NotOwned);
        }

        return track;
    }
}
