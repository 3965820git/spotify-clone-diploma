'use client'

import { TrackPlayer } from '@/widgets/player/TrackPlayer'
import { usePlayerStore } from '@/shared/lib/store/playerStore'

export default function PlayPage() {
  const setUrl = usePlayerStore((s) => s.setUrl)
  const setPlaying = usePlayerStore((s) => s.setPlaying)

  return (
    <div className="p-8 space-y-6">
      <h1 className="text-2xl font-bold">🎵 Player Playground</h1>

      <div className="space-x-4">
        <button
          className="rounded bg-green-600 px-4 py-2 text-white"
          onClick={() => {
            setUrl(
              'https://cdn.bitmovin.com/content/assets/art-of-motion-dash-hls-progressive/m3u8s/f08e80da-bf1d-4e3d-8899-f0f6155f6efa-audio-only.m3u8'
            )
            setPlaying(true)
          }}
        >
          ▶ Play Test Stream
        </button>

        <button
          className="rounded bg-neutral-700 px-4 py-2 text-white"
          onClick={() => setPlaying(false)}
        >
          ⏸ Pause
        </button>
      </div>

      <TrackPlayer />
    </div>
  )
}

