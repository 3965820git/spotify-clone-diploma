using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Playlists.Domain.Aggregates.Playlists.Exceptions;

public sealed class InvalidPlaylistMetadataDomainException(string message)
    : DomainExceptionBase(message);
