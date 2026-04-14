using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Delete;

public sealed record DeleteMoodCommand(
    Guid MoodId)
    : ICatalogPersistentCommand<DeleteMoodCommandResult>;
