using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.UnlinkCover;

public sealed record UnlinkCoverFromMoodCommand(
    Guid MoodId)
    : ICatalogPersistentCommand<UnlinkCoverFromMoodCommandResult>;
