'use client'

import { useQuery } from '@tanstack/react-query'
import { getTrackDetails } from '@/shared/api/tracks'

export function useCurrentTrackDetails(trackId: string | null) {
  return useQuery({
    queryKey: ['track-details', trackId],
    queryFn: () => getTrackDetails(trackId as string),
    enabled: Boolean(trackId),
    staleTime: 60_000,
  })
}