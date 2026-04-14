using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Delete;

internal sealed class DeleteMoodCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<DeleteMoodCommand, DeleteMoodCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<DeleteMoodCommandResult>> Handle(
        DeleteMoodCommand request,
        CancellationToken cancellationToken)
    {
        Mood? mood = await _unit.Moods.GetByIdAsync(
            MoodId.From(request.MoodId),
            cancellationToken);
        if (mood is null)
        {
            return Result.Failure<DeleteMoodCommandResult>(MoodErrors.NotFound);
        }

        mood.PrepareForDeletion();
        await _unit.Moods.DeleteAsync(mood, cancellationToken);

        return new DeleteMoodCommandResult();
    }
}
