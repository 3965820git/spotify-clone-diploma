# API Routes

Base URL: `/api/v1`

## Accounts Module

### Authentication

| Method | Endpoint                  | Description                       | Request Body                | Response                     |
| :----- | :------------------------ | :-------------------------------- | :-------------------------- | :--------------------------- |
| `POST` | `/accounts/auth/register` | Register a new user account       | `RegisterUserRequest`       | `RegisterUserResponse`       |
| `POST` | `/accounts/auth/login`    | Log in and obtain tokens          | `LoginUserRequest`          | `LoginUserResponse`          |
| `POST` | `/accounts/auth/logout`   | Log out and revoke current tokens | `LogoutUserRequest`         | `LogoutUserResponse`         |
| `POST` | `/accounts/auth/refresh`  | Rotate refresh token              | `RotateRefreshTokenRequest` | `RotateRefreshTokenResponse` |
| `POST` | `/accounts/auth/revoke`   | Revoke a refresh token            | `RevokeRefreshTokenRequest` | `RevokeRefreshTokenResponse` |

### Verification

| Method | Endpoint                              | Description                     | Request Body                       | Response                            |
| :----- | :------------------------------------ | :------------------------------ | :--------------------------------- | :---------------------------------- |
| `POST` | `/accounts/verification/email/send`   | Send email verification code    | `SendEmailVerificationCodeRequest` | `SendEmailVerificationCodeResponse` |
| `POST` | `/accounts/verification/email/verify` | Verify email with code          | `VerifyUserEmailRequest`           | `VerifyUserEmailResponse`           |
| `POST` | `/accounts/verification/phone/send`   | Send phone verification code    | `SendPhoneVerificationCodeRequest` | `SendPhoneVerificationCodeResponse` |
| `POST` | `/accounts/verification/phone/verify` | Verify phone with code          | `VerifyUserPhoneRequest`           | `VerifyUserPhoneResponse`           |
| `GET`  | `/accounts/verification/status`       | Get current verification status | -                                  | `GetUserVerificationStatusResponse` |

### User Profile (Me)

| Method  | Endpoint                    | Description                        | Request Body                   | Response                        |
| :------ | :-------------------------- | :--------------------------------- | :----------------------------- | :------------------------------ |
| `GET`   | `/accounts/me`              | Get current user's private profile | -                              | `GetCurrentUserProfileResponse` |
| `PATCH` | `/accounts/me/username`     | Change username                    | `ChangeUserNameRequest`        | `ChangeUserNameResponse`        |
| `PATCH` | `/accounts/me/password`     | Change password                    | `ChangeUserPasswordRequest`    | `ChangeUserPasswordResponse`    |
| `PATCH` | `/accounts/me/display-name` | Change display name                | `ChangeUserDisplayNameRequest` | `ChangeUserDisplayNameResponse` |
| `PATCH` | `/accounts/me/avatar`       | Change avatar image                | `ChangeUserAvatarRequest`      | `ChangeUserAvatarResponse`      |
| `PATCH` | `/accounts/me/email`        | Change email address               | `ChangeUserEmailRequest`       | `ChangeUserEmailResponse`       |
| `PATCH` | `/accounts/me/phone`        | Change phone number                | `ChangeUserPhoneNumberRequest` | `ChangeUserPhoneNumberResponse` |

### Users (Public)

| Method   | Endpoint                   | Description                 | Request Body               | Response                       |
| :------- | :------------------------- | :-------------------------- | :------------------------- | :----------------------------- |
| `GET`    | `/accounts/users/{userId}` | Get a user's public profile | -                          | `GetUserPublicProfileResponse` |
| `GET`    | `/accounts/users`          | List users (Admin/Search)   | `ListUsersRequest` (Query) | `ListUsersResponse`            |
| `DELETE` | `/accounts/users/{userId}` | Delete a user (Admin)       | -                          | `DeleteUsersResponse`          |

---

## Catalog Module

### Tracks

| Method   | Endpoint                                | Description                  | Request Body                        | Response                             |
| :------- | :-------------------------------------- | :--------------------------- | :---------------------------------- | :----------------------------------- |
| `POST`   | `/tracks`                               | Create a new track           | `CreateTrackRequest`                | `CreateTrackResponse`                |
| `GET`    | `/tracks/{id}`                          | Get track details            | -                                   | `TrackDetails`                       |
| `DELETE` | `/tracks/{id}`                          | Delete a track               | -                                   | -                                    |
| `POST`   | `/tracks/unlink-audio-file`             | Unlink track from audio file | -                                   | -                                    |
| `PATCH`  | `/tracks/{id}/correct-title`            | Correct track title          | `CorrectTrackTitleRequest`          | -                                    |
| `POST`   | `/tracks/{id}/explicit`                 | Mark track as explicit       | -                                   | -                                    |
| `DELETE` | `/tracks/{id}/explicit`                 | Unmark track as explicit     | -                                   | -                                    |

### Albums

| Method   | Endpoint                                | Description                   | Request Body                      | Response                            |
| :------- | :-------------------------------------- | :---------------------------- | :-------------------------------- | :---------------------------------- |
| `POST`   | `/albums`                               | Create album                  | `CreateAlbumRequest`              | `CreateAlbumResponse`               |
| `GET`    | `/albums/{id}`                          | Get album details             | -                                 | `AlbumDetails`                      |
| `DELETE` | `/albums/{id}`                          | Delete an album               | -                                 | -                                   |
| `PUT`    | `/albums/{id}/cover`                    | Link album to new cover image | `LinkAlbumToNewCoverImageRequest` | -                                   |
| `POST`   | `/albums/{id}/publish`                  | Publish album                 | `PublishAlbumRequest`             | -                                   |
| `DELETE` | `/albums/{id}/publish`                  | Unpublish album               | `UnpublishAlbumRequest`           | -                                   |
| `POST`   | `/albums/{id}/tracks`                   | Add track to album            | `AddTrackToAlbumRequest`          | -                                   |
| `DELETE` | `/albums/{id}/tracks/{trackId}`         | Remove track from album       | `RemoveTrackFromAlbumRequest`     | -                                   |
| `POST`   | `/albums/{id}/tracks/{tId}/move`        | Move track in album           | `MoveTrackInAlbumRequest`         | -                                   |
| `PATCH`  | `/albums/{id}/title`                    | Correct album title           | `CorrectAlbumTitleRequest`        | -                                   |
| `PATCH`  | `/albums/{id}/release`                  | Reschedule album release      | `RescheduleAlbumReleaseRequest`   | -                                   |

### Artists

| Method   | Endpoint                                | Description                      | Request Body                      | Response                     |
| :------- | :-------------------------------------- | :-------------------------       | :-------------------------------- | :--------------------------- |
| `POST`   | `/artists`                              | Create a new artist              | `CreateArtistRequest`             | `CreateArtistResponse`       |
| `GET`    | `/artists/{id}`                         | Get artist details               | -                                 | `ArtistDetails`              |
| `GET`    | `/artists/{id}/albums`                  | List artist's albums             | -                                 | `ArtistAlbumsList`           |
| `PUT`    | `/artists/{id}/avatar`                  | Link artist to new avatar        | `LinkArtistToNewAvatarRequest`    | -                            |
| `DELETE` | `/artists/{id}/avatar`                  | Unlink artist from avatar        | -                                 | -                            |
| `PUT`    | `/artists/{id}/banner`                  | Link artist to new banner        | `LinkArtistToNewBannerRequest`    | -                            |
| `DELETE` | `/artists/{id}/banner`                  | Unlink artist from banner        | -                                 | -                            |
| `DELETE` | `/artists/{id}`                         | Ban an artist                    | -                                 | -                            |
| `POST`   | `/artists/{id}/unban`                   | Unban an artist                  | -                                 | -                            |
| `POST`   | `/artists/{id}/verify`                  | Verify an artist                 | -                                 | -                            |
| `DELETE` | `/artists/{id}/verify`                  | Unverify an artist               | -                                 | -                            |
| `PUT`    | `/artists/{id}`                         | Edit artist profile info         | `EditArtistProfileRequest`        | -                            |
| `POST`   | `/artists/{id}/gallery`                 | Add gallery image to artist      | `AddGalleryImageToArtistRequest`  | -                            |
| `DELETE` | `/artists/{id}/gallery/{imageId}`       | Remove gallery image from artist | -                                 | -                            |

### Genres

| Method   | Endpoint                                | Description              | Request Body                    | Response                         |
| :------- | :-------------------------------------- | :----------------------- | :------------------------------ | :------------------------------- |
| `POST`   | `/genres`                               | Create a new genre       | `CreateGenreRequest`            | `CreateGenreResponse`            |
| `GET`    | `/genres`                               | List genres              | -                               | `GenreList`                      |
| `GET`    | `/genres/{id}`                          | Get genre details        | -                               | `GenreDetails`                   |
| `DELETE` | `/genres/{id}`                          | Delete a genre           | -                               | -                                |
| `PATCH`  | `/genres/{id}/name`                     | Rename genre             | `RenameGenreRequest`            | -                                |
| `PATCH`  | `/genres/{id}/description`              | Update genre description | `UpdateGenreDescriptionRequest` | -                                |
| `PUT`    | `/genres/{id}/cover`                    | Link genre to new cover  | `LinkGenreToNewCoverRequest`    | -                                |

### Moods

| Method   | Endpoint                                | Description              | Request Body                    | Response                         |
| :------- | :-------------------------------------- | :----------------------- | :------------------------------ | :------------------------------- |
| `POST`   | `/moods`                                | Create a new mood        | `CreateMoodRequest`             | `CreateMoodResponse`             |
| `GET`    | `/moods`                                | List moods               | -                               | `MoodList`                       |
| `GET`    | `/moods/{id}`                           | Get mood details         | -                               | `MoodDetails`                    |
| `DELETE` | `/moods/{id}`                           | Delete a mood            | -                               | -                                |
| `PATCH`  | `/moods/{id}/name`                      | Rename mood              | `RenameMoodRequest`             | -                                |
| `PATCH`  | `/moods/{id}/description`               | Update mood description  | `UpdateMoodDescriptionRequest`  | -                                |
| `PUT`    | `/moods/{id}/cover`                     | Link mood to new cover   | `LinkMoodToNewCoverRequest`     | -                                |

---

## Playlists Module

### Playlist Management

| Method   | Endpoint                              | Description                          | Request Body                       | Response                            |
| :------- | :------------------------------------ | :----------------------------------- | :--------------------------------- | :---------------------------------- |
| `GET`    | `/playlists/{playlistId}`             | Get playlist details                 | -                                  | `GetPlaylistDetailsResponse`        |
| `POST`   | `/playlists`                          | Create a new playlist                | `CreatePlaylistRequest`            | `CreatePlaylistResponse`            |
| `DELETE` | `/playlists/{playlistId}`             | Delete a playlist                    | -                                  | `DeletePlaylistResponse`            |
| `PATCH`  | `/playlists/{playlistId}/name`        | Rename playlist                      | `RenamePlaylistRequest`            | `RenamePlaylistResponse`            |
| `PATCH`  | `/playlists/{playlistId}/description` | Change description                   | `ChangePlaylistDescriptionRequest` | `ChangePlaylistDescriptionResponse` |
| `PATCH`  | `/playlists/{playlistId}/visibility`  | Update public/private status         | `UpdatePlaylistVisibilityRequest`  | `UpdatePlaylistVisibilityResponse`  |
| `PUT`    | `/playlists/{playlistId}/cover`       | Change cover image                   | `ChangePlaylistCoverRequest`       | `ChangePlaylistCoverResponse`       |
| `GET`    | `/playlists/me`                       | List current user's playlists        | `ListUserPlaylistsRequest` (Query) | `ListUserPlaylistsResponse`         |
| `GET`    | `/playlists/users/{userId}`           | List another user's public playlists | `ListUserPlaylistsRequest` (Query) | `ListUserPlaylistsResponse`         |

### Playlist Tracks

| Method   | Endpoint                                   | Description                | Request Body                     | Response                          |
| :------- | :----------------------------------------- | :------------------------- | :------------------------------- | :-------------------------------- |
| `POST`   | `/playlists/{playlistId}/tracks`           | Add track to playlist      | `AddTrackToPlaylistRequest`      | `AddTrackToPlaylistResponse`      |
| `DELETE` | `/playlists/{playlistId}/tracks/{trackId}` | Remove track from playlist | `RemoveTrackFromPlaylistRequest` | `RemoveTrackFromPlaylistResponse` |
| `PUT`    | `/playlists/{playlistId}/tracks/reorder`   | Reorder tracks             | `ReorderPlaylistTracksRequest`   | `ReorderPlaylistTracksResponse`   |

---

## Streaming Module

### Playback

| Method  | Endpoint                       | Description              | Request Body                    | Response                         |
| :------ | :----------------------------- | :----------------------- | :------------------------------ | :------------------------------- |
| `POST`  | `/streaming/playback/start`    | Start playback session   | `StartPlaybackRequest`          | `StartPlaybackResponse`          |
| `POST`  | `/streaming/playback/end`      | End playback session     | `EndPlaybackRequest`            | `EndPlaybackResponse`            |
| `PATCH` | `/streaming/playback/position` | Update playback progress | `UpdatePlaybackPositionRequest` | `UpdatePlaybackPositionResponse` |
| `GET`   | `/streaming/playback/session`  | Get current session info | -                               | `GetPlaybackSessionResponse`     |

### History & Likes

| Method   | Endpoint                            | Description                   | Request Body                       | Response                    |
| :------- | :---------------------------------- | :---------------------------- | :--------------------------------- | :-------------------------- |
| `POST`   | `/streaming/history`                | Record playback history entry | `RecordPlaybackRequest`            | `RecordPlaybackResponse`    |
| `GET`    | `/streaming/history/recent`         | Get recently played tracks    | `GetRecentlyPlayedRequest` (Query) | `GetRecentlyPlayedResponse` |
| `POST`   | `/streaming/likes/tracks/{trackId}` | Like a track                  | -                                  | `LikeTrackResponse`         |
| `DELETE` | `/streaming/likes/tracks/{trackId}` | Unlike a track                | -                                  | `UnlikeTrackResponse`       |
| `GET`    | `/streaming/likes/tracks`           | List liked tracks             | `ListLikedTracksRequest` (Query)   | `ListLikedTracksResponse`   |

### Media Assets (Internal/Admin)

| Method | Endpoint                  | Description        | Request Body              | Response                   |
| :----- | :------------------------ | :----------------- | :------------------------ | :------------------------- |
| `POST` | `/streaming/assets/audio` | Upload audio asset | `UploadAudioAssetRequest` | `UploadAudioAssetResponse` |
| `POST` | `/streaming/assets/image` | Upload image asset | `UploadImageAssetRequest` | `UploadImageAssetResponse` |

---

## Discovery & Browse Module

_Endpoints responsible for the "Home" page and content discovery._

### Browse

| Method | Endpoint                                    | Description                             | Request Body | Response                       |
| :----- | :------------------------------------------ | :-------------------------------------- | :----------- | :----------------------------- |
| `GET`  | `/browse/new-releases`                      | Get list of new album/single releases   | -            | `GetNewReleasesResponse`       |
| `GET`  | `/browse/featured-playlists`                | Get editor's picks (featured playlists) | -            | `GetFeaturedPlaylistsResponse` |
| `GET`  | `/browse/categories`                        | List all categories (Mood, Party, Rock) | -            | `GetCategoriesResponse`        |
| `GET`  | `/browse/categories/{categoryId}`           | Get single category details             | -            | `GetCategoryResponse`          |
| `GET`  | `/browse/categories/{categoryId}/playlists` | Get playlists for a specific category   | -            | `GetCategoryPlaylistsResponse` |

### Recommendations

| Method | Endpoint                                 | Description                                                       | Request Body                        | Response                         |
| :----- | :--------------------------------------- | :---------------------------------------------------------------- | :---------------------------------- | :------------------------------- |
| `GET`  | `/recommendations`                       | Generate recommendations based on seeds (genres, artists, tracks) | `GetRecommendationsRequest` (Query) | `GetRecommendationsResponse`     |
| `GET`  | `/recommendations/available-genre-seeds` | Get list of available genres for seeding                          | -                                   | `GetAvailableGenreSeedsResponse` |

---

## User Library Module

_Endpoints for managing the user's personal collection (Saved Albums, Followed Artists)._

### Saved Albums

| Method   | Endpoint              | Description                             | Request Body                      | Response                   |
| :------- | :-------------------- | :-------------------------------------- | :-------------------------------- | :------------------------- |
| `PUT`    | `/me/albums`          | Save one or more albums to library      | `SaveAlbumsRequest` (IDs)         | -                          |
| `DELETE` | `/me/albums`          | Remove albums from library              | `RemoveAlbumsRequest` (IDs)       | -                          |
| `GET`    | `/me/albums`          | Get user's saved albums                 | -                                 | `GetSavedAlbumsResponse`   |
| `GET`    | `/me/albums/contains` | Check if user has saved specific albums | `CheckSavedAlbumsRequest` (Query) | `CheckSavedAlbumsResponse` |

### Follows (Consolidated)

| Method   | Endpoint                 | Description                                     | Request Body                              | Response                 |
| :------- | :----------------------- | :---------------------------------------------- | :---------------------------------------- | :----------------------- |
| `GET`    | `/me/following`          | Get followed artists or users                   | `GetFollowingRequest` (Type: artist/user) | `GetFollowingResponse`   |
| `PUT`    | `/me/following`          | Follow artists or users                         | `FollowRequest` (Type, IDs)               | -                        |
| `DELETE` | `/me/following`          | Unfollow artists or users                       | `UnfollowRequest` (Type, IDs)             | -                        |
| `GET`    | `/me/following/contains` | Check if current user follows specific entities | `CheckFollowingRequest` (Type, IDs)       | `CheckFollowingResponse` |

---

## Extended Streaming (Player & Queue)

_Additions to the Streaming module for real-time playback control._

### Player State & Queue

| Method | Endpoint              | Description                                  | Request Body                   | Response                   |
| :----- | :-------------------- | :------------------------------------------- | :----------------------------- | :------------------------- |
| `GET`  | `/me/player`          | Get information about current playback state | -                              | `GetPlaybackStateResponse` |
| `GET`  | `/me/player/queue`    | Get the user's current playback queue        | -                              | `GetQueueResponse`         |
| `POST` | `/me/player/queue`    | Add an item to the end of the queue          | `AddToQueueRequest` (Uri/Id)   | -                          |
| `PUT`  | `/me/player/shuffle`  | Toggle shuffle mode                          | `ToggleShuffleRequest` (State) | -                          |
| `PUT`  | `/me/player/repeat`   | Set repeat mode (off/track/context)          | `SetRepeatModeRequest` (State) | -                          |
| `POST` | `/me/player/next`     | Skip to next track                           | -                              | -                          |
| `POST` | `/me/player/previous` | Skip to previous track                       | -                              | -                          |

### Lyrics

| Method | Endpoint                   | Description                   | Request Body | Response                 |
| :----- | :------------------------- | :---------------------------- | :----------- | :----------------------- |
| `GET`  | `/tracks/{trackId}/lyrics` | Get synced lyrics for a track | -            | `GetTrackLyricsResponse` |

---

## Search Module (Refined)

| Method   | Endpoint          | Description                                               | Request Body                   | Response                |
| :------- | :---------------- | :-------------------------------------------------------- | :----------------------------- | :---------------------- |
| `GET`    | `/search`         | Main search endpoint (tracks, albums, artists, playlists) | `SearchRequest` (Query, Types) | `SearchResponse`        |
| `GET`    | `/search/suggest` | Autocomplete suggestions for search bar                   | `SearchSuggestRequest` (Query) | `SearchSuggestResponse` |
| `DELETE` | `/search/recent`  | Clear recent search history                               | -                              | -                       |

---

## Social Module (Future)

| Method   | Endpoint                           | Description        | Request Body | Response |
| :------- | :--------------------------------- | :----------------- | :----------- | :------- |
| `POST`   | `/social/follow/user/{userId}`     | Follow a user      | -            | -        |
| `DELETE` | `/social/follow/user/{userId}`     | Unfollow a user    | -            | -        |
| `POST`   | `/social/follow/artist/{artistId}` | Follow an artist   | -            | -        |
| `DELETE` | `/social/follow/artist/{artistId}` | Unfollow an artist | -            | -        |

---

## Backoffice / Admin Panel

_Routes accessible only to users with 'Admin' or 'Moderator' roles._

### Dashboard

| Method | Endpoint                 | Description                                          | Request Body | Response                    |
| :----- | :----------------------- | :--------------------------------------------------- | :----------- | :-------------------------- |
| `GET`  | `/admin/stats/dashboard` | Get high-level system stats (DAU, MAU, Stream Count) | -            | `GetDashboardStatsResponse` |
| `GET`  | `/admin/stats/streaming` | Get real-time streaming metrics                      | -            | `GetStreamingStatsResponse` |

### User Management

| Method | Endpoint                      | Description                                       | Request Body                        | Response                  |
| :----- | :---------------------------- | :------------------------------------------------ | :---------------------------------- | :------------------------ |
| `GET`  | `/admin/users`                | Advanced user search and filtering                | `AdminListUsersRequest`             | `AdminListUsersResponse`  |
| `POST` | `/admin/users/{userId}/ban`   | Ban/Suspend a user account                        | `BanUserRequest` (Reason, Duration) | `BanUserResponse`         |
| `POST` | `/admin/users/{userId}/unban` | Restore a suspended user                          | -                                   | -                         |
| `PUT`  | `/admin/users/{userId}/roles` | Update user roles (e.g., Promote to Admin/Artist) | `UpdateUserRolesRequest`            | `UpdateUserRolesResponse` |

### Catalog Management

| Method   | Endpoint                                   | Description                                      | Request Body                | Response                 |
| :------- | :----------------------------------------- | :----------------------------------------------- | :-------------------------- | :----------------------- |
| `POST`   | `/admin/catalog/categories`                | Create a new Browse Category                     | `CreateCategoryRequest`     | `CreateCategoryResponse` |
| `PUT`    | `/admin/catalog/categories/{categoryId}`   | Update category details                          | `UpdateCategoryRequest`     | `UpdateCategoryResponse` |
| `POST`   | `/admin/catalog/featured`                  | Set featured items for Home page                 | `SetFeaturedContentRequest` | -                        |
| `POST`   | `/admin/catalog/artists/{artistId}/verify` | Manually verify an artist                        | -                           | -                        |
| `DELETE` | `/admin/catalog/takedown`                  | DMCA/Copyright takedown (Hides content globally) | `ContentTakedownRequest`    | -                        |
