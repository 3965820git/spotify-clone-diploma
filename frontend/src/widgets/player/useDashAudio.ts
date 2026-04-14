'use client'

import { useEffect, useRef } from 'react'

export function useDashAudio(audio: HTMLAudioElement | null, url: string | null) {
  const playerRef = useRef<any>(null)

  useEffect(() => {
    let disposed = false

    const run = async () => {
      if (!audio || !url) return

      // ✅ dynamic import only in browser/runtime
      const dashjs = await import('dashjs')
      if (disposed) return

      const player = (dashjs as any).MediaPlayer().create()
      playerRef.current = player

      player.initialize(audio, url, false)
    }

    run()

    return () => {
      disposed = true
      try {
        playerRef.current?.reset?.()
      } catch {}
      playerRef.current = null
    }
  }, [audio, url])
}