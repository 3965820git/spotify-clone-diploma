'use client'

import { useEffect, useRef } from 'react'
import { usePlaybackQuery, usePlaybackQueueQuery } from '@/features/player/api/usePlaybackQueries'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'

export function useAudioPlayer() {
  const audioRef = useRef<HTMLAudioElement | null>(null)
  const lastLoadedUrlRef = useRef<string | null>(null)

  const playbackQuery = usePlaybackQuery(true)
  const queueQuery = usePlaybackQueueQuery(true)

  const {
    playback,
    currentStreamUrl,
    startPositionMs,
    isBlockedByAnotherDevice,
    setPlayback,
    setQueue,
    setIsPlayerReady,
  } = usePlayerStore()

  const {
    nextTrackMutation,
    syncPositionMutation,
  } = usePlaybackActions()

  useEffect(() => {
    if (playbackQuery.data) {
      setPlayback(playbackQuery.data)
    }
  }, [playbackQuery.data, setPlayback])

  useEffect(() => {
    if (queueQuery.data) {
      setQueue(queueQuery.data)
    }
  }, [queueQuery.data, setQueue])

  useEffect(() => {
    const audio = audioRef.current

    if (!audio || !currentStreamUrl) {
      return
    }

    if (lastLoadedUrlRef.current === currentStreamUrl) {
      return
    }

    lastLoadedUrlRef.current = currentStreamUrl
    setIsPlayerReady(false)

    // 🔥 НАШ КОСТЫЛЬ: Игнорируем currentStreamUrl от бэкенда и подставляем прямой MP3
    const myTestUrl = "https://minio.spotify-diploma-2026.duckdns.org/audio/472a5729-15d0-4a2b-b87a-b07be353fadb/Requiem_For_A_Dream_-_Requiem_For_A_Dream_-_OST_%28SkySound.cc%29.mp3"
    audio.src = myTestUrl
    console.log("🔥 ВНИМАНИЕ: Включен режим 'Заплатка для диплома'. Играет Реквием из MinIO!")

    audio.load()

    const handleLoadedMetadata = async () => {
      if (typeof startPositionMs === 'number') {
        audio.currentTime = startPositionMs / 1000
      }

      setIsPlayerReady(true)

      if (playback?.isPlaying && !isBlockedByAnotherDevice) {
        try {
          await audio.play()
        } catch (error) {
          console.error('Audio play error:', error)
        }
      }
    }

    audio.addEventListener('loadedmetadata', handleLoadedMetadata)

    return () => {
      audio.removeEventListener('loadedmetadata', handleLoadedMetadata)
    }
  }, [
    currentStreamUrl,
    startPositionMs,
    playback?.isPlaying,
    isBlockedByAnotherDevice,
    setIsPlayerReady,
  ])

  useEffect(() => {
    const audio = audioRef.current

    if (!audio) {
      return
    }

    if (isBlockedByAnotherDevice) {
      audio.pause()
      return
    }

    if (playback?.isPlaying) {
      audio.play().catch((error) => {
        console.error('Resume audio error:', error)
      })
    } else {
      audio.pause()
    }
  }, [playback?.isPlaying, isBlockedByAnotherDevice])

  useEffect(() => {
    const audio = audioRef.current

    if (!audio) {
      return
    }

    const handleEnded = async () => {
      try {
        await nextTrackMutation.mutateAsync()
      } catch (error) {
        console.error('Next track error:', error)
      }
    }

    audio.addEventListener('ended', handleEnded)

    return () => {
      audio.removeEventListener('ended', handleEnded)
    }
  }, [nextTrackMutation])

  useEffect(() => {
    const audio = audioRef.current

    if (!audio || !playback?.isPlaying || isBlockedByAnotherDevice) {
      return
    }

    const intervalId = window.setInterval(() => {
      const positionMs = Math.floor(audio.currentTime * 1000)
      syncPositionMutation.mutate(positionMs)
    }, 5000)

    return () => {
      window.clearInterval(intervalId)
    }
  }, [playback?.isPlaying, isBlockedByAnotherDevice, syncPositionMutation])

  return {
    audioRef,
    playbackQuery,
    queueQuery,
  }
}
