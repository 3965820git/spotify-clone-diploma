using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Rename;

public sealed record RenameMoodCommand(
    Guid MoodId,
    string Name)
    : ICatalogPersistentCommand<RenameMoodCommandResult>;
