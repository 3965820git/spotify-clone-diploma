import { apiClient } from '@/shared/api/client'

export type PlaylistCoverImage = {
  imageId: string
  width: number
  height: number
  fileType: string
  sizeInBytes: number
}

export type PlaylistSummary = {
  id: string
  name: string
  description: string | null
  isPublic: boolean
  customCoverImageId: PlaylistCoverImage | null
  generatedCoverImageIds: string[]
}

export type CurrentUserPlaylistsResponse = {
  playlists: PlaylistSummary[]
}

export type PlaylistDetailsResponse = {
  id: string
  name: string
  description: string | null
  ownerId: string
  isPublic: boolean
  customCoverImageId: PlaylistCoverImage | null
  generatedCoverImageIds: string[]
  collaborators: Array<{
    id: string
    name: string
    avatarImageId: string | null
  }>
  tracks: Array<{
    id: string
    position: number
  }>
}

export type CreatePlaylistResponse = {
  playlistId: string
}

export type UpdatePlaylistRequest = {
  name: string
  description: string | null
  isPublic: boolean
}

export type AddTrackToPlaylistRequest = {
  trackId: string
}

export type LinkPlaylistCoverRequest = {
  imageId: string
  imageWidth: number
  imageHeight: number
  imageFileType: string
  imageSizeInBytes: number
}

export async function getCurrentUserPlaylists() {
  const { data } = await apiClient.get<CurrentUserPlaylistsResponse>(
    '/api/v1/me/playlists'
  )
  return data
}

export async function getPlaylist(id: string) {
  const { data } = await apiClient.get<PlaylistDetailsResponse>(
    `/api/v1/playlists/${id}`
  )
  return data
}

export async function createPlaylist() {
  const { data } = await apiClient.post<CreatePlaylistResponse>(
    '/api/v1/playlists'
  )
  return data
}

export async function updatePlaylist(id: string, body: UpdatePlaylistRequest) {
  await apiClient.put(`/api/v1/playlists/${id}`, body)
}

export async function deletePlaylist(id: string) {
  await apiClient.delete(`/api/v1/playlists/${id}`)
}

export async function addTrackToPlaylist(
  id: string,
  body: AddTrackToPlaylistRequest
) {
  await apiClient.post(`/api/v1/playlists/${id}/tracks`, body)
}

export async function removeTrackFromPlaylist(id: string, trackId: string) {
  await apiClient.delete(`/api/v1/playlists/${id}/tracks/${trackId}`)
}

export async function linkPlaylistCover(
  id: string,
  body: LinkPlaylistCoverRequest
) {
  await apiClient.put(`/api/v1/playlists/${id}/cover`, body)
}