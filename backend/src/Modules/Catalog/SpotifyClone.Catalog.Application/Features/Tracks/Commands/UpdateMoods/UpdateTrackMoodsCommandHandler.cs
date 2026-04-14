using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Artists;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateMoods;

internal sealed class UpdateTrackMoodsCommandHandler(
    ICatalogUnitOfWork unit,
    ICurrentUser currentUser)
    : ICommandHandler<UpdateTrackMoodsCommand, UpdateTrackMoodsCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<UpdateTrackMoodsCommandResult>> Handle(
        UpdateTrackMoodsCommand request,
        CancellationToken cancellationToken)
    {
        Track? track = await _unit.Tracks.GetByIdAsync(
            TrackId.From(request.TrackId),
            cancellationToken);
        if (track is null)
        {
            return Result.Failure<UpdateTrackMoodsCommandResult>(TrackErrors.NotFound);
        }

        IEnumerable<Artist> artists = await _unit.Artists.GetAllByIdsAsync(
            track.MainArtists,
            cancellationToken);

        if ((!_currentUser.IsAuthenticated || artists.Any(a => a.OwnerId?.Value != _currentUser.Id)) &&
            !_currentUser.IsInRole(UserRoles.Admin))
        {
            return Result.Failure<UpdateTrackMoodsCommandResult>(AlbumErrors.NotOwned);
        }

        IEnumerable<Mood> newMoods = await _unit.Moods.GetByIdsAsync(
            request.Moods.Select(a => MoodId.From(a)),
            cancellationToken);
        if (newMoods.Count() != request.Moods.Count())
        {
            return Result.Failure<UpdateTrackMoodsCommandResult>(MoodErrors.NotFound);
        }

        track.UpdateMoods([.. newMoods.Select(a => a.Id)]);

        return new UpdateTrackMoodsCommandResult();
    }
}
