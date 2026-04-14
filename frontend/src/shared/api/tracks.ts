import { apiClient } from '@/shared/api/client'

export type TrackArtist = {
  id: string
  name: string
  status: string
  ownerId: string | null
  avatar: string | null
}

export type TrackDetailsResponse = {
  id: string
  title: string
  duration: string | null
  releaseDate: string | null
  containsExplicitContent: boolean
  status: string
  audioFileId: string | null
  albumId: string | null
  mainArtists: TrackArtist[]
  featuredArtists: TrackArtist[]
  genres: Array<{
    id: string
    name: string
  }>
  moods: Array<{
    id: string
    name: string
  }>
}

export async function getTrackDetails(id: string) {
  const { data } = await apiClient.get<TrackDetailsResponse>(`/api/v1/tracks/${id}`)
  return data
}