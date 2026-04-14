
export type AdminStats = {
  followers: number
  tracks: number
  listens: number
}

export type AdminGenreOption = {
  id: string
  name: string
}

export type AdminTrackItem = {
  id: string
  title: string
  artist: string
  status: 'published' | 'pending'
  audioFileId: string | null
  albumId: string | null
  duration: string | null
}

export type AdminAnalyticsSummary = {
  publishedTracks: number
  followers: number
  listens: number
  likes: number
}

export type AdminAnalyticsProgressItem = {
  id: string
  title: string
  description: string
  value: number
}

export type AdminAnalyticsTipItem = {
  id: string
  title: string
  description: string
  icon: 'track' | 'share'
}