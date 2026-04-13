using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Create;

internal sealed class CreateMoodCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<CreateMoodCommand, CreateMoodCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<CreateMoodCommandResult>> Handle(
        CreateMoodCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _unit.Moods.IsNameUniqueAsync(request.Name, cancellationToken))
        {
            return Result.Failure<CreateMoodCommandResult>(MoodErrors.InvalidName);
        }

        var mood = Mood.Create(
            MoodId.From(Guid.NewGuid()),
            request.Name);

        await _unit.Moods.AddAsync(mood, cancellationToken);

        return new CreateMoodCommandResult(mood.Id.Value);
    }
}
