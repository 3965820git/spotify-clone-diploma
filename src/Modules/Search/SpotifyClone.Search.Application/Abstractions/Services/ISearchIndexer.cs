namespace SpotifyClone.Search.Application.Abstractions.Services;

public interface ISearchIndexer
{
    Task IndexDocumentAsync<T>(
        string indexName,
        T document,
        CancellationToken cancellationToken = default);

    Task DeleteDocumentAsync(
        string indexName,
        string documentId,
        CancellationToken cancellationToken = default);
}
