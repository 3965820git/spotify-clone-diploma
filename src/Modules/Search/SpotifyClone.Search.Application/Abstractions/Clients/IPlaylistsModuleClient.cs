using SpotifyClone.Shared.Kernel.Contracts.Playlists;

namespace SpotifyClone.Search.Application.Abstractions.Clients;

public interface IPlaylistsModuleClient
{
    Task<IEnumerable<PlaylistSharedDto>> GetAllPlaylistsAsync(
        CancellationToken cancellationToken = default);
}
