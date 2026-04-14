import { apiClient } from '@/shared/api/client'



export type ArtistsListResponse = {
  artists: {
    items: AdminArtist[]
    page: number
    pageSize: number
    totalCount: number
    hasNextPage: boolean
    hasPreviousPage: boolean
  }
}
export type AdminArtist = {
  id: string
  name: string
  status: string
  ownerId: string
  avatar: string | null
}



export type AdminGenre = {
  id: string
  name: string
  cover?: {
    imageId: string
    width: number
    height: number
    fileType: string
    sizeInBytes: number
  } | null
}

export type AdminMood = {
  id: string
  name: string
  cover?: {
    imageId: string
    width: number
    height: number
    fileType: string
    sizeInBytes: number
  } | null
}

export type AdminTrack = {
  id: string
  title: string
  duration: string | null
  releaseDate: string | null
  containsExplicitContent: boolean
  status: string
  audioFileId: string | null
  albumId: string | null
  mainArtists: string[]
  featuredArtists: string[]
}

export type TracksListResponse = {
  tracks: {
    items: AdminTrack[]
    page: number
    pageSize: number
    totalCount: number
    hasNextPage: boolean
    hasPreviousPage: boolean
  }
}

export type CreateAlbumRequest = {
  title: string
  mainArtistIds: string[]
}

export type CreateAlbumResponse = {
  albumId: string
}

export type CreateTrackRequest = {
  albumId: string
  containsExplicitContent: boolean
  genres: string[]
  mainArtists: string[]
  moods: string[]
  title: string
  featuredArtists: string[]
}

export type CreateTrackResponse = {
  trackId: string
}
export type MyTrack = {
  id: string
  title: string
  duration: string | null
  releaseDate: string | null
  containsExplicitContent: boolean
  status: string
  audioFileId: string | null
  albumId: string | null
  ownerId: string
  mainArtistIds: string[]
  featuredArtistIds: string[]
  genreIds: string[]
  moodIds: string[]
}

export type MyTracksResponse = {
  tracks: {
    items: MyTrack[]
    page: number
    pageSize: number
    totalCount: number
    hasNextPage: boolean
    hasPreviousPage: boolean
  }
}

export async function getMyTracks() {
  const { data } = await apiClient.get<MyTracksResponse>('/api/v1/me/tracks', {
    params: {
      page: 1,
      pageSize: 100,
    },
  })
  return data
}

export async function getArtists() {
  const { data } = await apiClient.get<AdminArtist[]>('/api/v1/artists', {
    params: { page: 1, pageSize: 100 },
  })
  return data
}

export async function getGenres() {
  const { data } = await apiClient.get<AdminGenre[]>('/api/v1/genres', {
    params: { page: 1, pageSize: 100 },
  })
  return data
}

export async function getMoods() {
  const { data } = await apiClient.get<AdminMood[]>('/api/v1/moods', {
    params: { page: 1, pageSize: 100 },
  })
  return data
}

export async function getTracks() {
  const { data } = await apiClient.get<TracksListResponse>('/api/v1/tracks', {
    params: { page: 1, pageSize: 100 },
  })
  return data
}

export async function createAlbum(body: CreateAlbumRequest) {
  const { data } = await apiClient.post<CreateAlbumResponse>('/api/v1/albums', body)
  return data
}

export async function createTrack(body: CreateTrackRequest) {
  const { data } = await apiClient.post<CreateTrackResponse>('/api/v1/tracks', body)
  return data
}

export async function uploadTrackAudio(trackId: string, file: File) {
  const formData = new FormData()
  formData.append('TrackId', trackId)
  formData.append('File', file)

  const { data } = await apiClient.post('/api/v1/media/audio', formData, {
    headers: { 'Content-Type': 'multipart/form-data' },
  })

  return data
}