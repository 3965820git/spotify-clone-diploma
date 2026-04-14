'use client'

import { useEffect, useRef, useState } from 'react'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'

type UseHlsAudioArgs = {
  url: string | null
  shouldPlay: boolean
  volume: number
  onShouldPlayChange?: (v: boolean) => void
}

export function useHlsAudio({
  url,
  shouldPlay,
  volume,
  onShouldPlayChange,
}: UseHlsAudioArgs) {
  const audioRef = useRef<HTMLAudioElement | null>(null)
  const hlsRef = useRef<any>(null)

  const setCurrentTimeSec = usePlayerStore((state) => state.setCurrentTimeSec)
  const setDurationSec = usePlayerStore((state) => state.setDurationSec)

  const [duration, setDuration] = useState(0)
  const [currentTime, setCurrentTime] = useState(0)
  const [bufferedPct, setBufferedPct] = useState(0)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return

    let disposed = false
    setError(null)

    if (hlsRef.current) {
      try {
        hlsRef.current.destroy()
      } catch {}
      hlsRef.current = null
    }

    audio.pause()
    audio.removeAttribute('src')
    audio.load()

    setDuration(0)
    setCurrentTime(0)
    setDurationSec(0)
    setCurrentTimeSec(0)

    if (!url) return

    const run = async () => {
      if (audio.canPlayType('application/vnd.apple.mpegurl')) {
        audio.src = url
        return
      }

      const mod = await import('hls.js')
      if (disposed) return
      const Hls = mod.default

      if (Hls.isSupported()) {
        const hls = new Hls()
        hlsRef.current = hls

        hls.on(Hls.Events.ERROR, (_evt: any, data: any) => {
          setError(`${data.type}: ${data.details}`)
        })

        hls.loadSource(url)
        hls.attachMedia(audio)
      } else {
        audio.src = url
      }
    }

    run()

    return () => {
      disposed = true
      if (hlsRef.current) {
        try {
          hlsRef.current.destroy()
        } catch {}
        hlsRef.current = null
      }
    }
  }, [url, setCurrentTimeSec, setDurationSec])

  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return
    audio.volume = volume / 100
  }, [volume])

  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return

    const run = async () => {
      if (!url) return

      if (shouldPlay && audio.paused) {
        try {
          await audio.play()
        } catch {
          onShouldPlayChange?.(false)
        }
      }

      if (!shouldPlay && !audio.paused) {
        audio.pause()
      }
    }

    run()
  }, [shouldPlay, url, onShouldPlayChange])

  const onLoadedMetadata = () => {
    const audio = audioRef.current
    if (!audio) return

    const nextDuration = audio.duration || 0
    setDuration(nextDuration)
    setDurationSec(nextDuration)
  }

  const onTimeUpdate = () => {
    const audio = audioRef.current
    if (!audio) return

    const nextCurrentTime = audio.currentTime || 0
    setCurrentTime(nextCurrentTime)
    setCurrentTimeSec(nextCurrentTime)

    try {
      if (audio.duration && audio.buffered.length > 0) {
        const end = audio.buffered.end(audio.buffered.length - 1)
        setBufferedPct(Math.min(100, (end / audio.duration) * 100))
      } else {
        setBufferedPct(0)
      }
    } catch {
      setBufferedPct(0)
    }
  }

  const seekTo = (seconds: number) => {
    const audio = audioRef.current
    if (!audio) return

    audio.currentTime = seconds
    setCurrentTime(seconds)
    setCurrentTimeSec(seconds)
  }

  return {
    audioRef,
    duration,
    currentTime,
    bufferedPct,
    error,
    onLoadedMetadata,
    onTimeUpdate,
    seekTo,
  }
}