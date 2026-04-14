using SpotifyClone.Catalog.Application.Abstractions;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.EditProfile;

public sealed record EditArtistProfileCommand(
    Guid ArtistId,
    string Name,
    string? Bio)
    : ICatalogPersistentCommand<EditArtistProfileCommandResult>;
