export type PlayerTab = 'next' | 'lyrics' | 'related' | null

export type QueueTrackItem = {
  id: string
  title: string
  duration: string | null
  albumId: string | null
  mainArtists: string[]
  moods?: string[]
}