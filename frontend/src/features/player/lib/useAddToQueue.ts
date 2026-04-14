'use client'

import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'

export function useAddToQueue() {
  const { addToQueueMutation } = usePlaybackActions()

  const addToQueue = async (trackId: string) => {
    await addToQueueMutation.mutateAsync(trackId)
  }

  return {
    addToQueue,
    isPending: addToQueueMutation.isPending,
  }
}