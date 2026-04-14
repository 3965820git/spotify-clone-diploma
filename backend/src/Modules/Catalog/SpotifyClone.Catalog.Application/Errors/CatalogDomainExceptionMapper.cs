using SpotifyClone.Catalog.Domain.Aggregates.Albums.Exceptions;
using SpotifyClone.Catalog.Domain.Aggregates.Artists.Exceptions;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.Exceptions;
using SpotifyClone.Catalog.Domain.Aggregates.Moods.Exceptions;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Exceptions;

namespace SpotifyClone.Catalog.Application.Errors;

public sealed class CatalogDomainExceptionMapper : IDomainExceptionMapper
{
    public Error MapToError(DomainExceptionBase domainException)
        => domainException switch
        {
            // Track
            InvalidImageMetadataDomainException => CommonCatalogErrors.InvalidImageMetadata,
            InvalidTrackDurationDomainException => TrackErrors.InvalidDuration,
            InvalidTrackGenresDomainException => TrackErrors.InvalidGenres,
            InvalidTrackMoodsDomainException => TrackErrors.InvalidMoods,
            InvalidTrackMainArtistsDomainException => TrackErrors.InvalidMainArtists,
            InvalidTrackTitleDomainException => TrackErrors.InvalidTitle,
            InvalidTrackReleaseDateDomainException => TrackErrors.InvalidReleaseDate,
            CannotPublishTrackDomainException => TrackErrors.CannotPublish,
            CannotMarkTrackAsReadyToPublishDomainException => TrackErrors.CannotMarkAsReadyToPublish,
            TrackAlreadyPublishedDomainException => TrackErrors.AlreadyPublished,
            TrackNotPublishedDomainException => TrackErrors.NotPublished,
            TrackAlreadyLinkedToAudioFileDomainException => TrackErrors.AlreadyLinkedToAudioFile,
            TrackAlreadyReleasedDomainException => TrackErrors.AlreadyReleased,
            TrackAlreadyAttachedToAnAlbumDomainException => TrackErrors.AlreadyAttachedToAnAlbum,

            // Album
            InvalidAlbumTitleDomainException => AlbumErrors.InvalidTitle,
            InvalidAlbumTypeDomainException => AlbumErrors.InvalidType,
            InvalidAlbumStatusDomainException => AlbumErrors.InvalidStatus,
            InvalidAlbumCoverImageDomainException => AlbumErrors.InvalidCoverImage,
            InvalidAlbumReleaseDateDomainException => AlbumErrors.InvalidReleaseDate,
            InvalidAlbumMainArtistsDomainException => AlbumErrors.InvalidMainArtists,
            InvalidAlbumTracksDomainException => AlbumErrors.InvalidTracks,
            InvalidAlbumTrackOrderDomainException => AlbumErrors.InvalidTrackOrder,
            AlbumAlreadyPublishedDomainException => AlbumErrors.AlreadyPublished,
            CannotPublishAlbumDomainException => AlbumErrors.CannotPublish,
            AlbumNotPublishedDomainException => AlbumErrors.NotPublished,
            MainArtistNotFoundInAlbumDomainException => AlbumErrors.MainArtistNotFound,
            InvalidTrackInAlbumDomainException => AlbumErrors.InvalidTrack,

            // Artist
            InvalidArtistAvatarImageDomainException => ArtistErrors.InvalidAvatarImage,
            InvalidArtistBannerImageDomainException => ArtistErrors.InvalidBannerImage,
            InvalidArtistGalleryImageDomainException => ArtistErrors.InvalidGalleryImage,
            InvalidArtistNameDomainException => ArtistErrors.InvalidName,
            InvalidArtistBioDomainException => ArtistErrors.InvalidBio,
            ArtistNotVerifiedDomainException => ArtistErrors.NotVerified,
            CannotVerifyArtistDomainException => ArtistErrors.CannotVerify,
            ArtistBannedDomainException => ArtistErrors.Banned,
            InvalidArtistStatusDomainException => ArtistErrors.InvalidStatus,

            // Genre
            InvalidGenreCoverImageDomainException => GenreErrors.InvalidCoverImage,
            InvalidGenreNameDomainException => GenreErrors.InvalidName,

            // Mood
            InvalidMoodCoverImageDomainException => MoodErrors.InvalidCoverImage,
            InvalidMoodNameDomainException => MoodErrors.InvalidName,

            // Other
            _ => CommonErrors.Unknown
        };
}
