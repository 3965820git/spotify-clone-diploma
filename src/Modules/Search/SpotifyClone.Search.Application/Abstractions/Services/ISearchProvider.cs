using SpotifyClone.Search.Application.Models;

namespace SpotifyClone.Search.Application.Abstractions.Services;

public interface ISearchProvider
{
    Task<SearchResult<T>> SearchAsync<T>(
        string indexName,
        string query,
        int limit = 50,
        CancellationToken cancellationToken = default);
}
