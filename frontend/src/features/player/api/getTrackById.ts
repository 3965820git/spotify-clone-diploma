import { PlayerTrack, TrackDetailsDto } from '@/features/player/model/types'

const API_BASE = process.env.NEXT_PUBLIC_API_BASE

function getArtistName(track: TrackDetailsDto): string {
  const main = track.mainArtists?.map((artist) => artist.name).filter(Boolean) ?? []
  const featured =
    track.featuredArtists?.map((artist) => artist.name).filter(Boolean) ?? []

  if (main.length === 0 && featured.length === 0) {
    return 'Unknown artist'
  }

  if (featured.length === 0) {
    return main.join(', ')
  }

  return `${main.join(', ')} feat. ${featured.join(', ')}`
}

function normalizeDuration(duration: number | null): number {
  if (typeof duration !== 'number' || Number.isNaN(duration)) {
    return 0
  }

  return duration
}

function normalizeTrack(track: TrackDetailsDto): PlayerTrack {
  return {
    id: track.id,
    title: track.title,
    artistName: getArtistName(track),
    durationSeconds: normalizeDuration(track.duration),
    containsExplicitContent: track.containsExplicitContent,
    coverUrl:
      'coverUrl' in track && typeof track.coverUrl === 'string'
        ? track.coverUrl
        : null,
    audioUrl: null,
    albumId: track.albumId,
    audioFileId: track.audioFileId,
  }
}

export async function getTrackById(id: string): Promise<PlayerTrack> {
  if (!API_BASE) {
    throw new Error('NEXT_PUBLIC_API_BASE is not configured')
  }

  const response = await fetch(`${API_BASE}/api/v1/tracks/${id}`, {
    method: 'GET',
    cache: 'no-store',
  })

  if (!response.ok) {
    throw new Error(`Failed to fetch track: ${response.status}`)
  }

  const data: TrackDetailsDto = await response.json()

  return normalizeTrack(data)
}