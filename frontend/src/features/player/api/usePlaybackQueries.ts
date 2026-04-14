'use client'

import { useQuery } from '@tanstack/react-query'
import { getPlayback, getPlaybackQueue } from '@/shared/api/playback'

export function usePlaybackQuery(enabled = true) {
  return useQuery({
    queryKey: ['playback'],
    queryFn: getPlayback,
    enabled,
    retry: false,
  })
}

export function usePlaybackQueueQuery(enabled = true) {
  return useQuery({
    queryKey: ['playback-queue'],
    queryFn: getPlaybackQueue,
    enabled,
    retry: false,
  })
}