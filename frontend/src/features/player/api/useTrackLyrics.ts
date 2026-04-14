import { useMemo } from 'react'
import { MOCK_LYRICS } from '../model/mock-lyrics'

export function useTrackLyrics(trackTitle?: string | null) {
  return useMemo(() => {
    if (!trackTitle) return null

    const normalizedTitle = trackTitle.trim().toLowerCase()

    const found = MOCK_LYRICS.find(
      (item) => item.trackTitle?.trim().toLowerCase() === normalizedTitle
    )

    return found?.lyrics ?? null
  }, [trackTitle])
}