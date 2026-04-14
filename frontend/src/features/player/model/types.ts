export type TrackArtistDto = {
  id: string
  name: string
  status: string
  avatar: string | null
}

export type TrackGenreDto = {
  id: string
  name: string
}

export type TrackMoodDto = {
  id: string
  name: string
}

export type TrackDetailsDto = {
  id: string
  title: string
  duration: number | null
  releaseDate: string | null
  containsExplicitContent: boolean
  status: string
  audioFileId: string | null
  albumId: string | null
  mainArtists: TrackArtistDto[]
  featuredArtists: TrackArtistDto[]
  genres: TrackGenreDto[]
  moods: TrackMoodDto[]
}

export type PlayerTrack = {
  id: string
  title: string
  artistName: string
  durationSeconds: number
  containsExplicitContent: boolean
  coverUrl: string | null
  audioUrl: string | null
  albumId: string | null
  audioFileId: string | null
}

export function mapTrackDtoToPlayerTrack(dto: {
  id: string
  title: string
  duration: number | null
  containsExplicitContent: boolean
  albumId: string | null
  audioFileId: string | null
  mainArtists?: { name: string }[]
  featuredArtists?: { name: string }[]
}): PlayerTrack {
  const main = dto.mainArtists?.map((artist) => artist.name) ?? []
  const featured = dto.featuredArtists?.map((artist) => artist.name) ?? []
  const artistName = [...main, ...featured].join(', ') || 'Unknown artist'

  return {
    id: dto.id,
    title: dto.title,
    artistName,
    durationSeconds: dto.duration ?? 0,
    containsExplicitContent: dto.containsExplicitContent,
    coverUrl: null,
    audioUrl: null,
    albumId: dto.albumId,
    audioFileId: dto.audioFileId,
  }
}