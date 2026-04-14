import { apiClient } from '@/shared/api/client'
import type {
  AddTrackToQueueRequest,
  DeviceOnlyRequest,
  PlaybackPositionRequest,
  PlaybackQueueResponse,
  PlaybackSession,
  PlaybackStreamResponse,
  StartPlaybackRequest,
  ToggleRepeatResponse,
  ToggleShuffleResponse,
} from '@/entities/playback/model/types'

export async function startPlayback(body: StartPlaybackRequest) {
  const { data } = await apiClient.post<PlaybackStreamResponse>(
    '/api/v1/me/playback/play',
    body
  )

  return data
}

export async function getPlayback() {
  const { data } = await apiClient.get<PlaybackSession>('/api/v1/me/playback')
  return data
}

export async function getPlaybackQueue() {
  const { data } = await apiClient.get<PlaybackQueueResponse>(
    '/api/v1/me/playback/queue'
  )

  return data
}

export async function addTrackToPlaybackQueue(body: AddTrackToQueueRequest) {
  await apiClient.post('/api/v1/me/playback/queue', body)
}

export async function removeTrackFromPlaybackQueue(trackId: string) {
  await apiClient.delete(`/api/v1/me/playback/queue/${trackId}`)
}

export async function resumePlayback(body: DeviceOnlyRequest) {
  await apiClient.post('/api/v1/me/playback/resume', body)
}

export async function pausePlayback(body: DeviceOnlyRequest) {
  await apiClient.post('/api/v1/me/playback/pause', body)
}

export async function syncPlaybackPosition(body: PlaybackPositionRequest) {
  await apiClient.patch('/api/v1/me/playback/sync', body)
}

export async function seekPlaybackPosition(body: PlaybackPositionRequest) {
  await apiClient.patch('/api/v1/me/playback/seek', body)
}

export async function skipToNextTrack(body: DeviceOnlyRequest) {
  const { data } = await apiClient.post<PlaybackStreamResponse>(
    '/api/v1/me/playback/next',
    body
  )

  return data
}

export async function skipToPreviousTrack(body: DeviceOnlyRequest) {
  const { data } = await apiClient.post<PlaybackStreamResponse>(
    '/api/v1/me/playback/previous',
    body
  )

  return data
}

export async function togglePlaybackShuffle(body: DeviceOnlyRequest) {
  const { data } = await apiClient.patch<ToggleShuffleResponse>(
    '/api/v1/me/playback/shuffle',
    body
  )

  return data
}

export async function togglePlaybackRepeat(body: DeviceOnlyRequest) {
  const { data } = await apiClient.patch<ToggleRepeatResponse>(
    '/api/v1/me/playback/repeat',
    body
  )

  return data
}