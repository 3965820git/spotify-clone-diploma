using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Exceptions;

namespace SpotifyClone.Playlists.Application.Errors;

public sealed class PlaylistsDomainExceptionMapper : IDomainExceptionMapper
{
    public Error MapToError(DomainExceptionBase domainException)
        => domainException switch
        {
            // Playlist
            InvalidImageMetadataDomainException => CommonPlaylistsErrors.InvalidImageMetadata,
            InvalidPlaylistMetadataDomainException => PlaylistErrors.InvalidMetadata,
            InvalidPlaylistCoverImageDomainException => PlaylistErrors.InvalidCover,
            InvalidPlaylistCollaboratorDomainException => PlaylistErrors.InvalidCollaborator,
            InvalidPlaylistTrackDomainException => PlaylistErrors.InvalidTrack,
            PlaylistIsNotUserGeneratedDomainException => PlaylistErrors.IsNotUserGenerated,

            // Other
            _ => CommonErrors.Unknown
        };
}
