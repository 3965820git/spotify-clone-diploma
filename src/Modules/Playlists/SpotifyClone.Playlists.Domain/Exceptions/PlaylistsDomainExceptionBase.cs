using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Playlists.Domain.Exceptions;

public abstract class PlaylistsDomainExceptionBase : DomainExceptionBase
{
    protected PlaylistsDomainExceptionBase(string message)
        : base(message)
    {
    }
}
