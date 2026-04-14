'use client'

import { TrackPlayer } from '@/features/player/ui/TrackPlayer'

export default function PlayClient() {
  return (
    <div className="p-8 space-y-6">
      <h1 className="text-2xl font-bold">🎵 Player Playground</h1>
      <p className="text-neutral-400">
        Тестовый плеер отключен в связи с переходом на новую систему очередей.
      </p>
      <TrackPlayer />
    </div>
  )
}
