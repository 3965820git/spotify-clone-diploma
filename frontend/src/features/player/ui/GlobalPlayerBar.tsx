'use client'

import { usePathname } from 'next/navigation'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'
import { PlayerBar } from '@/features/player/ui/PlayerBar'

const ROOT_PATHS = new Set([
  '/home',
  '/search',
  '/premium',
  '/library',
  '/profile',
])

export function GlobalPlayerBar() {
  const pathname = usePathname()
  const currentTrack = usePlayerStore((state) => state.queue?.currentTrack ?? null)

  if (!currentTrack) return null

  if (!pathname || !ROOT_PATHS.has(pathname)) {
    return null
  }

  return <PlayerBar />
}