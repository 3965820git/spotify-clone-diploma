'use client'

import { useEffect, useMemo, useRef, useState } from 'react'
import Hls from 'hls.js'
import { Button } from '@/components/ui/button'
import { Slider } from '@/components/ui/slider'
import { usePlayerStore } from '@/shared/lib/store/playerStore'

const DEFAULT_TEST_STREAM =
  'https://cdn.bitmovin.com/content/assets/art-of-motion-dash-hls-progressive/m3u8s/f08e80da-bf1d-4e3d-8899-f0f6155f6efa-audio-only.m3u8'

export function TrackPlayer() {
  const audioRef = useRef<HTMLAudioElement | null>(null)
  const hlsRef = useRef<Hls | null>(null)

  const url = usePlayerStore((s) => s.url) ?? DEFAULT_TEST_STREAM
  const isPlaying = usePlayerStore((s) => s.isPlaying)
  const volume = usePlayerStore((s) => s.volume)
  const currentTime = usePlayerStore((s) => s.currentTime)
  const duration = usePlayerStore((s) => s.duration)

  const setPlaying = usePlayerStore((s) => s.setPlaying)
  const setVolume = usePlayerStore((s) => s.setVolume)
  const setCurrentTime = usePlayerStore((s) => s.setCurrentTime)
  const setDuration = usePlayerStore((s) => s.setDuration)

  const [bufferedPct, setBufferedPct] = useState(0)

  // 1) Підключення стріму: ТІЛЬКИ при зміні url
  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return

    // прибираємо старий hls
    if (hlsRef.current) {
      hlsRef.current.destroy()
      hlsRef.current = null
    }

    // очищаємо src
    audio.pause()
    audio.removeAttribute('src')
    audio.load()

    if (audio.canPlayType('application/vnd.apple.mpegurl')) {
      // Safari / iOS native HLS
      audio.src = url
    } else if (Hls.isSupported()) {
      const hls = new Hls()
      hlsRef.current = hls
      hls.loadSource(url)
      hls.attachMedia(audio)
    } else {
      // fallback: спробуємо напряму
      audio.src = url
    }

    // якщо в store було "isPlaying=true", пробуємо продовжити після зміни треку
    const tryAutoplay = async () => {
      if (!audioRef.current) return
      if (!isPlaying) return
      try {
        await audioRef.current.play()
      } catch {
        // браузер може заблокувати autoplay, це ок
        setPlaying(false)
      }
    }
    tryAutoplay()

    return () => {
      if (hlsRef.current) {
        hlsRef.current.destroy()
        hlsRef.current = null
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [url])

  // 2) Гучність: окремо, щоб НЕ перезавантажувати HLS
  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return
    audio.volume = volume / 100
  }, [volume])

  // 3) Якщо isPlaying в store змінюється (наприклад з іншої сторінки) — синхронізуємо
  useEffect(() => {
    const audio = audioRef.current
    if (!audio) return

    const run = async () => {
      if (isPlaying && audio.paused) {
        try {
          await audio.play()
        } catch {
          setPlaying(false)
        }
      }
      if (!isPlaying && !audio.paused) {
        audio.pause()
      }
    }
    run()
  }, [isPlaying, setPlaying])

  const togglePlay = async () => {
    const audio = audioRef.current
    if (!audio) return

    if (audio.paused) {
      try {
        await audio.play()
        setPlaying(true)
      } catch {
        setPlaying(false)
      }
    } else {
      audio.pause()
      setPlaying(false)
    }
  }

  const seekTo = (seconds: number) => {
    const audio = audioRef.current
    if (!audio) return
    audio.currentTime = seconds
    setCurrentTime(seconds)
  }

  const fmt = (s: number) => {
    if (!Number.isFinite(s) || s < 0) return '0:00'
    const m = Math.floor(s / 60)
    const sec = Math.floor(s % 60)
    return `${m}:${sec.toString().padStart(2, '0')}`
  }

  // прогрес-слайдер (0..duration)
  const progressValue = useMemo(() => {
    if (!duration) return [0]
    return [Math.min(currentTime, duration)]
  }, [currentTime, duration])

  return (
    <div className="w-full max-w-xl space-y-4 rounded-lg border bg-neutral-950 p-4 text-white">
      <audio
        ref={audioRef}
        onLoadedMetadata={(e) => {
          const d = e.currentTarget.duration || 0
          setDuration(d)
        }}
        onTimeUpdate={(e) => {
          const t = e.currentTarget.currentTime || 0
          setCurrentTime(t)

          // buffered %
          const a = e.currentTarget
          try {
            if (a.duration && a.buffered.length > 0) {
              const end = a.buffered.end(a.buffered.length - 1)
              setBufferedPct(Math.min(100, (end / a.duration) * 100))
            } else {
              setBufferedPct(0)
            }
          } catch {
            setBufferedPct(0)
          }
        }}
      />

      <div className="flex items-center gap-4">
        <Button onClick={togglePlay}>{isPlaying ? 'Pause' : 'Play'}</Button>

        <div className="flex items-center gap-3">
          <span className="w-12 text-right text-xs opacity-70">{volume}%</span>
          <div className="w-40">
            <Slider
              value={[volume]}
              min={0}
              max={100}
              step={1}
              onValueChange={([v]) => setVolume(v)}
            />
          </div>
        </div>

        <span className="ml-auto text-sm opacity-70">
          {fmt(currentTime)} / {fmt(duration)}
        </span>
      </div>

      {/* Progress + buffered bar */}
      <div className="space-y-2">
        <div className="h-1 w-full rounded bg-neutral-800 overflow-hidden">
          <div className="h-full bg-neutral-600" style={{ width: `${bufferedPct}%` }} />
        </div>

        <Slider
          value={progressValue}
          min={0}
          max={duration || 0}
          step={1}
          onValueChange={([v]) => seekTo(v)}
          disabled={!duration}
        />
      </div>

      <p className="text-xs opacity-60 break-all">URL: {url}</p>
    </div>
  )
}
