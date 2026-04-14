export type TrackArtistDto = {
  id: string
  name: string
  status: string
  ownerId: string | null
  avatar: string | null
}

export type TrackTagDto = {
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
  genres: TrackTagDto[]
  moods: TrackTagDto[]
}