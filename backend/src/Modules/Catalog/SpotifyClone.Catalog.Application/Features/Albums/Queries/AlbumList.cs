namespace SpotifyClone.Catalog.Application.Features.Albums.Queries;

public sealed record AlbumList(
    IReadOnlyCollection<AlbumSummary> albums);
