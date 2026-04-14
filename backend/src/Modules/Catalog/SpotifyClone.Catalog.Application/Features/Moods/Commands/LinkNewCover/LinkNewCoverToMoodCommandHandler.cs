using SpotifyClone.Catalog.Application.Abstractions;
using SpotifyClone.Catalog.Application.Errors;
using SpotifyClone.Catalog.Domain.Aggregates.Moods;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.LinkNewCover;

internal sealed class LinkNewCoverToMoodCommandHandler(
    ICatalogUnitOfWork unit)
    : ICommandHandler<LinkNewCoverToMoodCommand, LinkNewCoverToMoodCommandResult>
{
    private readonly ICatalogUnitOfWork _unit = unit;

    public async Task<Result<LinkNewCoverToMoodCommandResult>> Handle(
        LinkNewCoverToMoodCommand request,
        CancellationToken cancellationToken)
    {
        Mood? mood = await _unit.Moods.GetByIdAsync(
            MoodId.From(request.MoodId),
            cancellationToken);
        if (mood is null)
        {
            return Result.Failure<LinkNewCoverToMoodCommandResult>(MoodErrors.NotFound);
        }

        mood.LinkNewCover(new MoodCoverImage(
            ImageId.From(request.ImageId),
            request.ImageWidth,
            request.ImageHeight,
            ImageFileType.From(request.ImageFileType),
            request.ImageSizeInBytes));

        return new LinkNewCoverToMoodCommandResult();
    }
}
