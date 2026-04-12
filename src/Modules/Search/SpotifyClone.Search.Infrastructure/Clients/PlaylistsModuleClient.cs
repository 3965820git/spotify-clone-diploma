using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using SpotifyClone.Search.Application.Abstractions.Clients;
using SpotifyClone.Shared.Kernel.Contracts.Playlists;

namespace SpotifyClone.Search.Infrastructure.Clients;

internal sealed class PlaylistsModuleClient(
    HttpClient httpClient,
    ILogger<PlaylistsModuleClient> logger)
    : IPlaylistsModuleClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<PlaylistsModuleClient> _logger = logger;

    public async Task<IEnumerable<PlaylistSharedDto>> GetAllPlaylistsAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<PlaylistSharedDto>? response
                = await _httpClient.GetFromJsonAsync<IEnumerable<PlaylistSharedDto>>(
                    "api/v1/shared/playlists", cancellationToken);

            return response ?? Enumerable.Empty<PlaylistSharedDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting Playlists from Playlists module");
            return Enumerable.Empty<PlaylistSharedDto>();
        }
    }
}
