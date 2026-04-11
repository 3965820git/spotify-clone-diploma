using SpotifyClone.Search.Application.Models.Documents;

namespace SpotifyClone.Search.Application.Features.GlobalSearch;

public sealed record GlobalSearchResponse(
    IEnumerable<TrackSearchDocument> Tracks,
    IEnumerable<AlbumSearchDocument> Albums,
    IEnumerable<ArtistSearchDocument> Artists,
    IEnumerable<PlaylistSearchDocument> Playlists,
    IEnumerable<UserSearchDocument> Users,
    IEnumerable<GenreSearchDocument> Genres,
    IEnumerable<MoodSearchDocument> Moods);
