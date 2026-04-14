'use client'

import { useEffect, useMemo, useRef } from 'react'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import { useHlsAudio } from '@/widgets/player/useHlsAudio'
import { useDashAudio } from '@/widgets/player/useDashAudio'

export function TrackPlayer() {
  const audioFallbackRef = useRef<HTMLAudioElement | null>(null)
  const lastSyncedSecondRef = useRef<number>(-1)
  const appliedStartPositionRef = useRef(false)
const currentTrackId = usePlayerStore((state) => state.currentTrackId)
  const currentStreamUrl = usePlayerStore((state) => state.currentStreamUrl)
  const playback = usePlayerStore((state) => state.playback)
  const startPositionMs = usePlayerStore((state) => state.startPositionMs)
  const setCurrentTimeSec = usePlayerStore((state) => state.setCurrentTimeSec)
  const setDurationSec = usePlayerStore((state) => state.setDurationSec)
  const setIsPlayerReady = usePlayerStore((state) => state.setIsPlayerReady)

  const { syncPositionMutation, nextTrackMutation, refreshPlaybackState } =
    usePlaybackActions()

  const isPlaying = playback?.isPlaying ?? false

  const isHls = useMemo(() => {
    if (!currentStreamUrl) return false

    return (
      currentStreamUrl.includes('.m3u8') ||
      currentStreamUrl.includes('format=hls') ||
      currentStreamUrl.includes('/hls')
    )
  }, [currentStreamUrl])

  const isDash = useMemo(() => {
    if (!currentStreamUrl) return false

    return (
      currentStreamUrl.includes('.mpd') ||
      currentStreamUrl.includes('format=dash') ||
      currentStreamUrl.includes('/dash')
    )
  }, [currentStreamUrl])

  const {
    audioRef: hlsAudioRef,
    onLoadedMetadata,
    onTimeUpdate,
  } = useHlsAudio({
    url: isHls ? currentStreamUrl : null,
    shouldPlay: isPlaying,
    volume: 100,
    onShouldPlayChange: () => {},
  })

  useDashAudio(audioFallbackRef.current, isDash ? currentStreamUrl : null)

  useEffect(() => {
    appliedStartPositionRef.current = false
    lastSyncedSecondRef.current = -1
    setCurrentTimeSec(0)
    setDurationSec(0)
    setIsPlayerReady(false)
  }, [currentStreamUrl, setCurrentTimeSec, setDurationSec, setIsPlayerReady])

  useEffect(() => {
    const audio = hlsAudioRef.current ?? audioFallbackRef.current
    if (!audio) return

    if (!currentStreamUrl) {
      audio.pause()
      audio.removeAttribute('src')
      audio.load()
      setIsPlayerReady(false)
      return
    }

    if (!isHls && !isDash) {
      if (audio.src !== currentStreamUrl) {
        audio.src = currentStreamUrl
        audio.load()
      }

      if (isPlaying) {
        void audio.play().catch(() => {})
      } else {
        audio.pause()
      }
    }
  }, [currentStreamUrl, isPlaying, isHls, isDash, hlsAudioRef, setIsPlayerReady])

  useEffect(() => {
    const audio = hlsAudioRef.current ?? audioFallbackRef.current
    if (!audio) return
    if (!currentStreamUrl) return
    if (startPositionMs == null) return
    if (appliedStartPositionRef.current) return

    const applyStartPosition = () => {
      const seconds = Number(startPositionMs) / 1000

      if (Number.isFinite(seconds) && seconds >= 0) {
        audio.currentTime = seconds
        setCurrentTimeSec(seconds)
        appliedStartPositionRef.current = true
      }
    }

    if (audio.readyState >= 1) {
      applyStartPosition()
    }
  }, [currentStreamUrl, startPositionMs, hlsAudioRef, setCurrentTimeSec])

  useEffect(() => {
    const audio = hlsAudioRef.current ?? audioFallbackRef.current
    if (!audio) return

    const handleLoaded = () => {
      onLoadedMetadata()
      setIsPlayerReady(true)

      if (!appliedStartPositionRef.current && startPositionMs != null) {
        const seconds = Number(startPositionMs) / 1000

        if (Number.isFinite(seconds) && seconds >= 0) {
          audio.currentTime = seconds
          setCurrentTimeSec(seconds)
          appliedStartPositionRef.current = true
        }
      }

      setDurationSec(Number.isFinite(audio.duration) ? audio.duration : 0)

      if (isPlaying) {
        void audio.play().catch(() => {})
      }
    }

const handleTime = () => {
  onTimeUpdate()

  const seconds = Number.isFinite(audio.currentTime) ? audio.currentTime : 0
  setCurrentTimeSec(seconds)

  const wholeSecond = Math.floor(seconds)

  if (!playback?.isPlaying || !currentStreamUrl || !currentTrackId) {
    return
  }

  if (wholeSecond !== lastSyncedSecondRef.current) {
    lastSyncedSecondRef.current = wholeSecond
    void syncPositionMutation.mutateAsync(Math.floor(seconds * 1000))
  }
}

    const handleEnded = async () => {
      try {
        await nextTrackMutation.mutateAsync()
      } catch {
        await refreshPlaybackState()
      }
    }

    audio.addEventListener('loadedmetadata', handleLoaded)
    audio.addEventListener('timeupdate', handleTime)
    audio.addEventListener('ended', handleEnded)

    return () => {
      audio.removeEventListener('loadedmetadata', handleLoaded)
      audio.removeEventListener('timeupdate', handleTime)
      audio.removeEventListener('ended', handleEnded)
    }
  }, [
    hlsAudioRef,
    isPlaying,
    nextTrackMutation,
    onLoadedMetadata,
    onTimeUpdate,
    refreshPlaybackState,
    setCurrentTimeSec,
    setDurationSec,
    setIsPlayerReady,
    startPositionMs,
    syncPositionMutation,
  ])

  return (
    <>
      <audio
        ref={hlsAudioRef}
        preload="metadata"
        onLoadedMetadata={onLoadedMetadata}
        className="hidden"
      />

      {!isHls ? (
        <audio
          ref={audioFallbackRef}
          preload="metadata"
          className="hidden"
        />
      ) : null}
    </>
  )
}