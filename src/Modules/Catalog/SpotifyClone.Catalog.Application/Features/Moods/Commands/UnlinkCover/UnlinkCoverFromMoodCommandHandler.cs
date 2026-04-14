using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.UnlinkCover;

internal sealed class UnlinkCoverFromMoodCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<UnlinkCoverFromMoodCommand, UnlinkCoverFromMoodCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<UnlinkCoverFromMoodCommandResult>> Handle(
        UnlinkCoverFromMoodCommand request,
        CancellationToken cancellationToken)
    {
        Mood? mood = await _unit.Moods.GetByIdAsync(
            MoodId.From(request.MoodId),
            cancellationToken);
        if (mood is null)
        {
            return Result.Failure<UnlinkCoverFromMoodCommandResult>(MoodErrors.NotFound);
        }

        mood.TryUnlinkCover();

        return new UnlinkCoverFromMoodCommandResult();
    }
}
