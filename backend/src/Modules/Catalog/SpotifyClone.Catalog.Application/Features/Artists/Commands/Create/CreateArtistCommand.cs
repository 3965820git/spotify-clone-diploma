using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.Create;

public sealed record CreateArtistCommand(
    string Name)
    : ICatalogPersistentCommand<CreateArtistCommandResult>;
