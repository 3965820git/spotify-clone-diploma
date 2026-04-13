using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.Exceptions;

public sealed class InvalidPlaylistCollaboratorDomainException(string message)
    : DomainExceptionBase(message);
