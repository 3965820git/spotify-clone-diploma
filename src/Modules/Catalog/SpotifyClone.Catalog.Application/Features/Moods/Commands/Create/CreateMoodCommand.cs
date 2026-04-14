using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Create;

public sealed record CreateMoodCommand(
    string Name)
    : ICatalogPersistentCommand<CreateMoodCommandResult>;
