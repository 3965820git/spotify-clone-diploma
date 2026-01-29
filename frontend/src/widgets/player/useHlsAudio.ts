'use client'

import { useEffect, useRef, useState } from 'react'
import Hls from 'hls.js'

type UseHlsAudioArgs = {
  url: string | null
  shouldPlay: boolean
  volume: number // 0..100
  onShouldPlayChange?: (v: boolean) => void
}

export function useHlsAudio({ url, shouldPlay, volume, onShouldPlayChange }: UseHlsAudioArgs) {
  const audioRef = useRef<HTMLAudioElement | null>(null)
  const hlsRef = useRef<Hls | null>(null)

  const [duration, setDuration] = useState(0)
  const [currentTime, setCurrentTime] = useState(0)
  const [bufferedPct, setBufferedPct] = useState(0)
  const [error, setError] = useState<string | null>(null)

  // підключення URL (тільки коли url змінюється)
  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return

    setError(null)

    if (hlsRef.current) {
      hlsRef.current.destroy()
      hlsRef.current = null
    }

    audio.pause()
    audio.removeAttribute('src')
    audio.load()

    if (!url) return

    if (audio.canPlayType('application/vnd.apple.mpegurl')) {
      audio.src = url
    } else if (Hls.isSupported()) {
      const hls = new Hls()
      hlsRef.current = hls

      hls.on(Hls.Events.ERROR, (_evt, data) => {
        // дуже базово: показуємо помилку
        setError(`${data.type}: ${data.details}`)
      })

      hls.loadSource(url)
      hls.attachMedia(audio)
    } else {
      audio.src = url
    }

    return () => {
      if (hlsRef.current) {
        hlsRef.current.destroy()
        hlsRef.current = null
      }
    }
  }, [url])

  // volume окремо (щоб не перезбирати HLS)
  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return
    audio.volume = volume / 100
  }, [volume])

  // play/pause синхронізація
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

  // events
  const onLoadedMetadata = () => {
    const audio = audioRef.current
    if (!audio) return
    setDuration(audio.duration || 0)
  }

  const onTimeUpdate = () => {
    const audio = audioRef.current
    if (!audio) return
    setCurrentTime(audio.currentTime || 0)

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
