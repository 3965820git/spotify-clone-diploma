'use client'

import { useMemo } from 'react'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'

export function usePlayerControls() {
  const { playback, queue, isBlockedByAnotherDevice, isLoading } = usePlayerStore()

  const {
    pausePlaybackMutation,
    resumePlaybackMutation,
    nextTrackMutation,
    previousTrackMutation,
    toggleShuffleMutation,
    toggleRepeatMutation,
  } = usePlaybackActions()

  const isPlaying = playback?.isPlaying ?? false
  const isShuffled = playback?.isShuffled ?? false
  const repeatMode = playback?.repeatMode ?? 'Off'

  const isDisabled = isBlockedByAnotherDevice || isLoading

  const currentTrack = queue?.currentTrack ?? null
  const nextTracks = queue?.tracksInQueue ?? []

  const canGoNext = nextTracks.length > 0 || repeatMode === 'All' || repeatMode === 'Track'
  const canGoPrevious = true

  const handleTogglePlay = async () => {
    if (isDisabled) {
      return
    }

    if (isPlaying) {
      await pausePlaybackMutation.mutateAsync()
      return
    }

    await resumePlaybackMutation.mutateAsync()
  }

  const handleNext = async () => {
    if (isDisabled || !canGoNext) {
      return
    }

    await nextTrackMutation.mutateAsync()
  }

  const handlePrevious = async () => {
    if (isDisabled || !canGoPrevious) {
      return
    }

    await previousTrackMutation.mutateAsync()
  }

  const handleToggleShuffle = async () => {
    if (isDisabled) {
      return
    }

    await toggleShuffleMutation.mutateAsync()
  }

  const handleToggleRepeat = async () => {
    if (isDisabled) {
      return
    }

    await toggleRepeatMutation.mutateAsync()
  }

  const isAnyPlaybackActionPending = useMemo(() => {
    return (
      pausePlaybackMutation.isPending ||
      resumePlaybackMutation.isPending ||
      nextTrackMutation.isPending ||
      previousTrackMutation.isPending ||
      toggleShuffleMutation.isPending ||
      toggleRepeatMutation.isPending
    )
  }, [
    pausePlaybackMutation.isPending,
    resumePlaybackMutation.isPending,
    nextTrackMutation.isPending,
    previousTrackMutation.isPending,
    toggleShuffleMutation.isPending,
    toggleRepeatMutation.isPending,
  ])

  return {
    playback,
    queue,
    currentTrack,
    nextTracks,

    isPlaying,
    isShuffled,
    repeatMode,

    isDisabled,
    isAnyPlaybackActionPending,
    canGoNext,
    canGoPrevious,

    handleTogglePlay,
    handleNext,
    handlePrevious,
    handleToggleShuffle,
    handleToggleRepeat,
  }
}