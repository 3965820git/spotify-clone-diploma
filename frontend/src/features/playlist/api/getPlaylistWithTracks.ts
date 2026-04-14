import { getPlaylist } from '@/shared/api/playlists'
import { getTrack } from '@/shared/api/tracks'

export type PlaylistTrackViewModel = {
  id: string
  position: number
  title: string
  duration: number | null
  artist: string
  image: string | null
  albumId: string | null
  containsExplicitContent: boolean
}

export type PlaylistDetailsViewModel = {
  id: string
  title: string
  description: string
  author: string
  year: string
  tracksCount: number
  cover: string | null
  tracks: PlaylistTrackViewModel[]
}

export async function getPlaylistWithTracks(
  playlistId: string,
): Promise<PlaylistDetailsViewModel> {
  const playlist = await getPlaylist(playlistId)

  const sortedTrackRefs = [...playlist.tracks].sort(
    (a, b) => a.position - b.position,
  )

  const trackDetails = await Promise.all(
    sortedTrackRefs.map((trackRef) => getTrack(trackRef.id)),
  )

  const tracks: PlaylistTrackViewModel[] = sortedTrackRefs.map(
    (trackRef, index) => {
      const track = trackDetails[index]

      return {
        id: track.id,
        position: trackRef.position,
        title: track.title,
        duration: track.duration,
        artist:
          track.mainArtists.length > 0
            ? track.mainArtists.map((artist) => artist.name).join(', ')
            : 'Unknown artist',
        image: null,
        albumId: track.albumId,
        containsExplicitContent: track.containsExplicitContent,
      }
    },
  )

  return {
    id: playlist.id,
    title: playlist.name,
    description: playlist.description ?? '',
    author:
      playlist.collaborators.length > 0
        ? playlist.collaborators.map((item) => item.name).join(', ')
        : 'Playlist',
    year: '',
    tracksCount: tracks.length,
    cover: null,
    tracks,
  }
}