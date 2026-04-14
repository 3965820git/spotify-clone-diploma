import { useQuery } from '@tanstack/react-query'
import { getMyTracks } from './admin.api'
import { getTrackById } from '@/features/player/api/getTrackById'
import type { AdminTrackItem } from './admin.types'
import type { MyTrack } from './admin.api'

function getTrackStatus(audioFileId: string | null): 'published' | 'pending' {
  return audioFileId ? 'published' : 'pending'
}

export function useAdminTracks() {
  return useQuery({
    queryKey: ['admin-my-tracks'],
    queryFn: async (): Promise<AdminTrackItem[]> => {
      const tracksRes = await getMyTracks()
      const tracks: MyTrack[] = tracksRes?.tracks?.items ?? []

      const detailedTracks: AdminTrackItem[] = await Promise.all(
        tracks.map(async (track): Promise<AdminTrackItem> => {
          try {
            const details = await getTrackById(track.id)

            return {
              id: track.id,
              title: track.title,
              artist: details.artistName || 'Unknown artist',
              status: getTrackStatus(track.audioFileId),
              audioFileId: track.audioFileId,
              albumId: track.albumId,
              duration: track.duration,
            }
          } catch {
            return {
              id: track.id,
              title: track.title,
              artist: 'Unknown artist',
              status: getTrackStatus(track.audioFileId),
              audioFileId: track.audioFileId,
              albumId: track.albumId,
              duration: track.duration,
            }
          }
        })
      )

      return detailedTracks
    },
  })
}