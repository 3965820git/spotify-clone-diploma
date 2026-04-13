using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.LinkNewCover;

public sealed record LinkNewCoverToMoodCommand(
    Guid MoodId,
    Guid ImageId,
    int ImageWidth,
    int ImageHeight,
    string ImageFileType,
    long ImageSizeInBytes)
    : ICatalogPersistentCommand<LinkNewCoverToMoodCommandResult>;
