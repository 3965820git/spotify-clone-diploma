using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Application.Features.Artists.Queries;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Queries.GetDetails;

internal sealed class GetTrackDetailsQueryHandler(
    ITrackReadService trackReadService,
    IArtistReadService artistReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetTrackDetailsQuery, TrackDetails>
{
    private readonly ITrackReadService _trackReadService = trackReadService;
    private readonly IArtistReadService _artistReadService = artistReadService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<TrackDetails>> Handle(
        GetTrackDetailsQuery request,
        CancellationToken cancellationToken)
    {
        TrackDetails? track = await _trackReadService.GetDetailsAsync(
            TrackId.From(request.TrackId),
            cancellationToken);
        if (track is null)
        {
            return Result.Failure<TrackDetails>(TrackErrors.NotFound);
        }

        IEnumerable<ArtistSummary> artists = await _artistReadService.GetAllByIdsAsync(
            [ ..track.MainArtists.Select(a => ArtistId.From(a.Id)),
              ..track.FeaturedArtists.Select(a => ArtistId.From(a.Id)) ],
            cancellationToken);

        if (!_currentUser.IsAuthenticated &&
            !artists.Any(a => a.OwnerId == _currentUser.Id) &&
            _currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<TrackDetails>(TrackErrors.NotOwned);
        }

        return track;
    }
}
