'use client'

import { useRouter } from 'next/navigation'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import type { PlaybackContextType } from '@/entities/playback/model/types'

type Props = {
  trackId: string
  contextType?: PlaybackContextType
  contextExternalId?: string | null
  className?: string
  children: React.ReactNode
}

export function OpenTrackButton({
  trackId,
  contextType = 'search',
  contextExternalId = null,
  className = '',
  children,
}: Props) {
  const router = useRouter()
  const { startPlaybackMutation } = usePlaybackActions()

  const handleClick = async () => {
    if (!trackId) return

    await startPlaybackMutation.mutateAsync({
      contextType,
      contextExternalId,
      startTrackId: trackId,
    })

    router.push('/player')
  }

  return (
    <button
      type="button"
      onClick={() => {
        void handleClick()
      }}
      className={className}
    >
      {children}
    </button>
  )
}