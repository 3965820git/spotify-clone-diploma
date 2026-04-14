'use client'

import type { PlaybackContextType } from '@/entities/playback/model/types'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'

type PlayTrackParams = {
  trackId: string
  contextType: PlaybackContextType
  contextExternalId: string | null
}

export function usePlayTrack() {
  const { startPlaybackMutation } = usePlaybackActions()

  const playTrack = async ({
    trackId,
    contextType,
    contextExternalId,
  }: PlayTrackParams) => {
    await startPlaybackMutation.mutateAsync({
      contextType,
      contextExternalId,
      startTrackId: trackId,
    })
  }

  return {
    playTrack,
    isPending: startPlaybackMutation.isPending,
  }
}