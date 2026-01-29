# Subdomains

## User & Identity Subdomain (Generic)

Capabilities related to account management and user identity.

### Authentication

- Register account
- Log in
- Log out
- Forgot password
- Email/phone verification

### User Profile

- Manage profile info (name, avatar, email, password, phone)
- View profile info (username, display name, avatar, role, registered at)

## Music Catalog Subdomain (Supporting)

Capabilities for managing and accessing music metadata.

### Tracks

- Load audio file URL from storage
- View track metadata (title, duration, artist(s), album, release date, stream count, lyrics)

### Albums

- View album metadata (cover art, title, release date, artists, track count, duration)
- View album tracklist

### Artists

- View artist profile (name, monthly listeners count)
- View artist's popular songs
- View artist discography (albums, EPs, singles)

### Genres
- View genre details (name, description, cover art)
- View genre's popular songs

## Playlists Subdomain (Core)

Capabilities required for playlist creation and manipulation.

### Playlist Management

- Create playlist
- Delete playlist
- Rename playlist
- Set playlist description
- Clone playlist

### Playlist Track Operations

- Add track to playlist
- Remove track from playlist
- Reorder tracks
- Prevent duplicates
- Set playlist cover
- Generate playlist cover automatically

### Playlist Ownership

- User-owned playlist
- Public playlists
- Collaborative playlists

### Playlist Views

- View all public playlists of user
- View playlist details (name, visibility, description, author(s), track count, duration)
- View playlist track list

## Playback & Streaming Subdomain (Supporting)

Everything related to playing audio.
Supporting because we don't use our own audio system to be unique.
“Can this audio be played right now, and how?”

### Playback Controls

- Play track
- Pause/Resume track
- Skip next/previous
- Shuffle
- Repeat (track/playlist)
- Adjust volume
- Auto-play next track
- Auto-pause when track ends

### Playback Pipeline

- Fetch audio stream URL
- Buffer audio
- Handle network loss (desktop)
- Track playback position
- Handle audio output device selection

### Playback History

- Record “recently played”
- Record timestamp of playback
- Record full playback vs partial playback
- List recently played items

## Search Subdomain (Generic)

Capabilities for music search.
When becomes complex - Supporting.

### Basic Search

- Search by track title
- Search by artist name
- Search by album name
- Search by playlist name

### Search Filters

- Filter by genre/year/duration

### Search Optimization

- Autocomplete
- Search history
- Popular searches

## Listening & Analytics Subdomain (Supporting)

## Recommendations Subdomain (Supporting)

With AI - Core subdomain.

- Recommended albums/playlists
- Related artists
- Daily Mix

## Social & Community Subdomain (Generic/Supporting)

“How does a user express preference or interact with content?”

- Follow artists
- Follow other users
- See artist/user activity
- Share playlist/album/track/artist
- Comments on playlist/album/track/artist
- Like/Unlike a track
- Show all liked tracks
