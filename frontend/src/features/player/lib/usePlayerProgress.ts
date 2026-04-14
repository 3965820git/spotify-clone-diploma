'use client'

import { useEffect, useMemo, useState } from 'react'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'

function formatTime(totalSeconds: number) {
  if (!Number.isFinite(totalSeconds) || totalSeconds < 0) {
    return '0 : 00'
  }

  const minutes = Math.floor(totalSeconds / 60)
  const seconds = Math.floor(totalSeconds % 60)

  return `${minutes} : ${seconds.toString().padStart(2, '0')}`
}

export function usePlayerProgress() {
  const currentTrackId = usePlayerStore((state) => state.currentTrackId)
  const currentTimeFromStore = usePlayerStore((state) => state.currentTimeSec)
  const durationFromStore = usePlayerStore((state) => state.durationSec)

  const { seekPositionMutation } = usePlaybackActions()

  const [isDragging, setIsDragging] = useState(false)
  const [dragValueSec, setDragValueSec] = useState(0)

  useEffect(() => {
    setIsDragging(false)
    setDragValueSec(0)
  }, [currentTrackId])

  const displayedTimeSec = isDragging ? dragValueSec : currentTimeFromStore
  const durationSec = durationFromStore > 0 ? durationFromStore : 0

  const progressPercent = useMemo(() => {
    if (!durationSec || durationSec <= 0) {
      return 0
    }

    return Math.min((displayedTimeSec / durationSec) * 100, 100)
  }, [displayedTimeSec, durationSec])

  const handleProgressChange = (value: number[]) => {
    const nextValue = value[0] ?? 0
    setIsDragging(true)
    setDragValueSec(nextValue)
  }

  const handleProgressCommit = async (value: number[]) => {
    const nextValueSec = value[0] ?? 0
    const nextValueMs = Math.floor(nextValueSec * 1000)

    const audio = document.querySelector('audio')

    if (audio) {
      audio.currentTime = nextValueSec
    }

    setDragValueSec(nextValueSec)
    setIsDragging(false)

    await seekPositionMutation.mutateAsync(nextValueMs)
  }

  return {
    currentTimeSec: displayedTimeSec,
    durationSec,
    progressPercent,
    formattedCurrentTime: formatTime(displayedTimeSec),
    formattedDuration: formatTime(durationSec),

    handleProgressChange,
    handleProgressCommit,
  }
}