export type PlaybackContextType = 'album' | 'playlist' | 'search'

export type RepeatMode = 'Off' | 'All' | 'Track' | string

export type PlaybackTrack = {
  id: string
  title: string
  duration: number | null
  releaseDate: string | null
  containsExplicitContent: boolean
  status: string
  audioFileId: string | null
  albumId: string | null
  mainArtists: string[]
  featuredArtists: string[]
}

export type PlaybackSession = {
  id: string
  userId: string
  trackId: string | null
  deviceId: string
  contextType: PlaybackContextType | string
  contextExternalId: string | null
  currentPositionMs: number
  isPlaying: boolean
  isShuffled: boolean
  repeatMode: RepeatMode
  updatedAtUtc: string
}

export type PlaybackQueueResponse = {
  currentTrack: PlaybackTrack | null
  tracksInQueue: PlaybackTrack[]
}

export type PlaybackStreamResponse = {
  hlsUrl: string | null
  dashUrl: string | null
  startPositionMs: number | null
  trackId: string | null
}

export type StartPlaybackRequest = {
  deviceId: string
  contextType: PlaybackContextType
  contextExternalId: string | null
  startTrackId: string | null
}

export type DeviceOnlyRequest = {
  deviceId: string
}

export type PlaybackPositionRequest = {
  deviceId: string
  positionMs: number
}

export type AddTrackToQueueRequest = {
  trackId: string
}

export type ToggleShuffleResponse = {
  isShuffled: boolean
}

export type ToggleRepeatResponse = {
  repeatMode: RepeatMode
}