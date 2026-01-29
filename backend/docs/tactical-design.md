# Bounded Contexts

## Shared Kernel

Responsible for storing cross-bounded-context terms, mostly value objects.

### UserId

Even if the property name is differ per bounded context (e.g. OwnerId, CollaboratorId), rules are not, so it's fine to simply reference this shared value object (almost) everywhere.

- Value: Guid

### TrackId

- Value: Guid

### ImageId

- Value: Guid

### ImageMetadata

- Width: Integer
- Height: Integer
- FileType: String

## Accounts BC

Responsible for user account lifecycle.

### User

A person who has an account in the system and can authenticate.

- Id: UserId (Value Object [SharedKernel](#shared-kernel))
- Username: String (ASP.NET Identity)
- PasswordHash: String (ASP.NET Identity)
- Email: String (ASP.NET Identity)
- Phone: String (ASP.NET Identity)
- Role: (ASP.NET Identity - "User", "Admin")
- EmailVerified: Boolean (ASP.NET Identity)
- PhoneVerified: Boolean (ASP.NET Identity)
- DisplayName: String
- BirthDate: DateTime
- Avatar: AvatarImage? (Value Object):
    - Metadata: ImageMetadata (Value Object [SharedKernel](#shared-kernel))
    - ImageId: ImageId (Value Object [SharedKernel](#shared-kernel))
- RefreshTokens: List<RefreshToken> (Infrastructure Entity, not part of the User aggregate):
    - Id: Guid
    - UserId: UserId (Value Object [SharedKernel](#shared-kernel))
    - TokenHash: String
    - CreatedAt: DateTime
    - ExpiresAt: DateTime
    - RevokedAt: DateTime?
    - ReplacedByTokenHash: String?
    - IsActive: Boolean (RevokedAt == null && ExpiresAt > DateTime.UtcNow)

Optional fields:
- RegisteredAt: DateTime (later add to ASP.NET Identity if necessary)
- UpdatedAt: DateTime (later add to Infrastructure if necessary)
- UserPreferences: store as a separate DB table, but better if stored in FrontEnd localStorage/cookies.

Optional entities:
- UserPlans: will be considered if there will be a Billing BC
- UserDevices: can be implemented as Infrastructure entity in the future

## Catalog BC

Responsible for defining and describing all musical content available in the system.

### Track

Individual audio recording.

- Id: TrackId (Value Object - [SharedKernel](#shared-kernel))
- Title: String
- Description: String?
- Duration: TimeSpan (AudioResource.Duration)
- ReleaseDate: DateOnly
- ExplicitContent: Boolean
- TrackNumber: Integer
- IsPublished: Boolean
- AudioFileId: AudioFileId (Value Object):
    - Value: Guid
- AlbumId: AlbumId (Value Object [Album](#album))
- MainArtists: List<ArtistId> (Value Object [Artist](#artist))
- FeaturedArtists: List<ArtistId> (Value Object [Artist](#artist))
- Genres: List<GenreId> (Aggregate Root [Genre](#genre))

### Album

Collection of tracks.

- Id: AlbumId (Value Object):
    - Value: Guid
- Title: String
- Description: String?
- ReleaseDate: DateOnly
- Type: AlbumType (Smart Enum):
    - Value: String("Single", "EP", "Album")
- Cover: AlbumCoverImage (Value Object):
    - Metadata: ImageMetadata (Value Object [SharedKernel](#shared-kernel))
    - ImageId: ImageId (Value Object [SharedKernel](#shared-kernel))
- MainArtists: List<ArtistId> (Value Object [Artist](#artist))

### Artist

Creator of tracks and albums.

- Id: ArtistId (Value Object):
    - Value: Guid
- Name: String
- Bio: String?
- IsVerified: Boolean
- Avatar: ArtistAvatarImage? (Value Object):
    - Metadata: ImageMetadata (Value Object [SharedKernel](#shared-kernel))
    - ImageId: ImageId (Value Object [SharedKernel](#shared-kernel))
- Banner: ArtistBannerImage? (Value Object):
    - Metadata: ImageMetadata (Value Object [SharedKernel](#shared-kernel))
    - ImageId: ImageId (Value Object [SharedKernel](#shared-kernel))
- Gallery: List<ArtistGalleryImage> (Value Object):
    - Metadata: ImageMetadata (Value Object [SharedKernel](#shared-kernel))
    - ImageId: ImageId (Value Object [SharedKernel](#shared-kernel))

### Genre

Category or style of music.

- Id (GenreId) (Value Object):
    - Value: Guid
- Name: String
- Description: String?
- Cover: GenreCoverImage (Value Object):
    - Metadata: ImageMetadata (Value Object [SharedKernel](#shared-kernel))
    - ImageFileId: ImageFileId (Value Object [SharedKernel](#shared-kernel))

## Playlists BC

### Playlist

A user-defined collection of tracks.

- Id: PlaylistId (Value Object):
    - Value: Guid
- OwnerId: UserId (Value Object - [SharedKernel](#shared-kernel))
- Name: String
- Description: String?
- Cover: PlaylistCoverImage? (Value Object):
    - Metadata: ImageMetadata (Value Object [SharedKernel](#shared-kernel))
    - ImageFileId: ImageFileId (Value Object [SharedKernel](#shared-kernel))
- IsPublic: Boolean
- Tracks: List<PlaylistTrack> (Entity):
    - PRIMARY KEY: (PlaylistId, Position)
    - TrackId: TrackId (Value Object - [SharedKernel](#shared-kernel))
    - Position: Integer
    - AddedAt: DateTime

## Streaming BC

Responsible for media assets (audio & image), playback sessions, playback history, likes (User <-> Track interaction), stream statistics (as facts, not analytics).

### AudioAsset

- Id: AudioFileId (Value Object):
    - Value: Guid
- Duration: TimeSpan
- Format: AudioFormat (Smart Enum):
    - Value: String ("MP3")
- BitrateKbps: int
- SampleRateHz: int
- Channels: int
- FileSizeBytes: long
- Checksum: string
- StorageKey: string
- IsActive: bool
- CreatedAt: DateTime

### ImageAsset

- Id: ImageId (Value Object - [SharedKernel](#shared-kernel))
- Width: Integer
- Height: Integer
- MimeType: String
- StorageKey: String
- IsActive: Boolean
- CreatedAt: DateTime

### PlaybackSession

- Id: PlaybackSessionId (Value Object):
    - Value: Guid
- UserId: UserId (Value Object - [SharedKernel](#shared-kernel))
- TrackId: TrackId (Value Object - [SharedKernel](#shared-kernel))
- StartedAt: DateTime
- EndedAt: DateTime?
- CurrentPositionMs: Integer
- IsPaused: Boolean
- ShuffleMode: Boolean (in future - Enumeration)
- RepeatMode: PlaybackRepeatMode (Enumeration):
    - RepeatOff
    - RepeatAll
    - RepeatOne
- DeviceType: PlaybackDeviceType (Enumeration):
    - Web
    - Desktop
    - Mobile

### PlaybackHistoryEntry

- Id: PlaybackHistoryEntryId (Value Object):
    - Value: Guid
- UserId: UserId (Value Object - [SharedKernel](#shared-kernel))
- TrackId: TrackId (Value Object - [SharedKernel](#shared-kernel))
- PlayedAt: DateTime
- PlayedSeconds: uint
- Completed: Boolean

### LikedTrack

- UserId: UserId (Value Object - [SharedKernel](#shared-kernel))
- TrackId: TrackId (Value Object - [SharedKernel](#shared-kernel))
- LikedAt: DateTime

## Search BC

## Analytics BC

## Recommendations BC

## Social BC

# Context Map

[Upstream] -> [Downstream]

- Identity -> Catalog
- Identity -> Playlists
- Catalog -> Playlists
