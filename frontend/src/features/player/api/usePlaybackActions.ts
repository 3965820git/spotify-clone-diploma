'use client'

import { useMutation, useQueryClient } from '@tanstack/react-query'
import {
  addTrackToPlaybackQueue,
  getPlayback,
  getPlaybackQueue,
  pausePlayback,
  removeTrackFromPlaybackQueue,
  resumePlayback,
  seekPlaybackPosition,
  skipToNextTrack,
  skipToPreviousTrack,
  startPlayback,
  syncPlaybackPosition,
  togglePlaybackRepeat,
  togglePlaybackShuffle,
} from '@/shared/api/playback'
import { getOrCreateDeviceId } from '@/shared/lib/device/getOrCreateDeviceId'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'
import type { PlaybackContextType } from '@/entities/playback/model/types'

export function usePlaybackActions() {
  const queryClient = useQueryClient()

  const {
    setPlayback,
    setQueue,
    setCurrentStreamUrl,
    setCurrentTrackId,
    setStartPositionMs,
    setIsLoading,
  } = usePlayerStore()

  const refreshPlaybackState = async () => {
    const [playback, queue] = await Promise.all([
      getPlayback().catch(() => null),
      getPlaybackQueue().catch(() => null),
    ])

    setPlayback(playback ?? null)
    setQueue(queue ?? null)

    if (playback?.trackId) {
      setCurrentTrackId(playback.trackId)
    } else {
      setCurrentTrackId(queue?.currentTrack?.id ?? null)
    }

    await queryClient.setQueryData(['playback'], playback)
    await queryClient.setQueryData(['playback-queue'], queue)

    return { playback, queue }
  }

  const startPlaybackMutation = useMutation({
    mutationFn: async (params: {
      contextType: PlaybackContextType
      contextExternalId: string | null
      startTrackId: string | null
    }) => {
      const deviceId = getOrCreateDeviceId()

      return startPlayback({
        deviceId,
        contextType: params.contextType,
        contextExternalId: params.contextExternalId,
        startTrackId: params.startTrackId,
      })
    },
    onMutate: () => {
      setIsLoading(true)
    },
    onSuccess: async (data) => {
      const streamUrl = data.hlsUrl ?? data.dashUrl ?? null

      setCurrentStreamUrl(streamUrl)
      setCurrentTrackId(data.trackId ?? null)
      setStartPositionMs(
        data.startPositionMs == null ? 0 : Number(data.startPositionMs)
      )

      await refreshPlaybackState()
    },
    onSettled: () => {
      setIsLoading(false)
    },
  })

  const pausePlaybackMutation = useMutation({
    mutationFn: async () => {
      const deviceId = getOrCreateDeviceId()
      return pausePlayback({ deviceId })
    },
    onSuccess: async () => {
      await refreshPlaybackState()
    },
  })

  const resumePlaybackMutation = useMutation({
    mutationFn: async () => {
      const deviceId = getOrCreateDeviceId()
      return resumePlayback({ deviceId })
    },
    onSuccess: async () => {
      await refreshPlaybackState()
    },
  })

  const nextTrackMutation = useMutation({
    mutationFn: async () => {
      const deviceId = getOrCreateDeviceId()
      return skipToNextTrack({ deviceId })
    },
    onSuccess: async (data) => {
      const streamUrl = data.hlsUrl ?? data.dashUrl ?? null

      setCurrentStreamUrl(streamUrl)
      setCurrentTrackId(data.trackId ?? null)
      setStartPositionMs(
        data.startPositionMs == null ? 0 : Number(data.startPositionMs)
      )

      await refreshPlaybackState()
    },
  })

  const previousTrackMutation = useMutation({
    mutationFn: async () => {
      const deviceId = getOrCreateDeviceId()
      return skipToPreviousTrack({ deviceId })
    },
    onSuccess: async (data) => {
      const streamUrl = data.hlsUrl ?? data.dashUrl ?? null

      setCurrentStreamUrl(streamUrl)
      setCurrentTrackId(data.trackId ?? null)
      setStartPositionMs(
        data.startPositionMs == null ? 0 : Number(data.startPositionMs)
      )

      await refreshPlaybackState()
    },
  })

  const toggleShuffleMutation = useMutation({
    mutationFn: async () => {
      const deviceId = getOrCreateDeviceId()
      return togglePlaybackShuffle({ deviceId })
    },
    onSuccess: async () => {
      await refreshPlaybackState()
    },
  })

  const toggleRepeatMutation = useMutation({
    mutationFn: async () => {
      const deviceId = getOrCreateDeviceId()
      return togglePlaybackRepeat({ deviceId })
    },
    onSuccess: async () => {
      await refreshPlaybackState()
    },
  })

  const addToQueueMutation = useMutation({
    mutationFn: async (trackId: string) => {
      return addTrackToPlaybackQueue({ trackId })
    },
    onSuccess: async () => {
      await refreshPlaybackState()
    },
  })

  const removeFromQueueMutation = useMutation({
    mutationFn: async (trackId: string) => {
      return removeTrackFromPlaybackQueue(trackId)
    },
    onSuccess: async () => {
      await refreshPlaybackState()
    },
  })

  const syncPositionMutation = useMutation({
    mutationFn: async (positionMs: number) => {
      const deviceId = getOrCreateDeviceId()
      return syncPlaybackPosition({ deviceId, positionMs })
    },
  })

  const seekPositionMutation = useMutation({
    mutationFn: async (positionMs: number) => {
      const deviceId = getOrCreateDeviceId()
      return seekPlaybackPosition({ deviceId, positionMs })
    },
    onSuccess: async () => {
      await refreshPlaybackState()
    },
  })

  return {
    startPlaybackMutation,
    pausePlaybackMutation,
    resumePlaybackMutation,
    nextTrackMutation,
    previousTrackMutation,
    toggleShuffleMutation,
    toggleRepeatMutation,
    addToQueueMutation,
    removeFromQueueMutation,
    syncPositionMutation,
    seekPositionMutation,
    refreshPlaybackState,
  }
}